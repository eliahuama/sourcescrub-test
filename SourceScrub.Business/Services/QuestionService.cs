using Microsoft.EntityFrameworkCore;
using SourceScrub.Business.Services.Interfaces;
using SourceScrub.Data;
using SourceScrub.Entities;

namespace SourceScrub.Business.Services
{
    public class QuestionService: IQuestionService
    {
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<Vote> _voteRepository;

        private readonly ApplicationDbContext _context;

        public QuestionService(
            IRepository<Question> questionRepository,
            IRepository<Answer> answerRepository,
            IRepository<Vote> voteRepository,
            IRepository<Tag> tagRepository,
            ApplicationDbContext context)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _voteRepository = voteRepository;
            _tagRepository = tagRepository;
            _context = context;
        }

        public IQueryable<Question> GetAll()
        {
            // thats a really bad thing I've done, but wanted you to see the object in-depth.
            return _context.Questions
                    .Include(q => q.Tags)
                    .Include(q => q.User)
                    .Include(q => q.Votes)
                        .ThenInclude(v => v.User)
                    .Include(q => q.Answers)
                        .ThenInclude(a => a.Votes)
                            .ThenInclude(v => v.User)
                    .Include(q => q.Answers)
                        .ThenInclude(a => a.User);
        }

        public async Task<Question?> GetAsync(int id)
        {
            return await GetAll().FirstOrDefaultAsync(q => q.Id == id);
        }

        public IQueryable<Question> GetByTags(ICollection<string> tags)
        {
            return GetAll().Where(q => q.Tags.Any(tag => tags.Contains(tag.Text)));
        }

        public Task<Question> AddAsync(Question question)
        {
            return _questionRepository.AddAsync(question);
        }

        public Task<ICollection<Question>> AddAsync(ICollection<Question> questions)
        {
            return _questionRepository.AddAsync(questions);
        }

        public async Task<Answer?> AddAnswerAsync(Answer answer)
        {
            var question = await GetAsync(answer.QuestionId);
            if (question == null) return null;
            return await _answerRepository.AddAsync(answer);
        }

        public async Task<bool> VoteAsync(int questionId, int userId, bool upvote)
        {
            var question = await _questionRepository.GetAsync(questionId);
            if (question == null) return false;

            var vote = new Vote { QuestionId = questionId, UserId = userId, Value = upvote ? 1 : -1 };
            await _voteRepository.AddAsync(vote);
            return true;
        }

        public async Task<bool> VoteAsync(int questionId, int answerId, int userId, bool upvote)
        {
            var question = await _questionRepository.GetAsync(questionId);
            var answer = await _answerRepository.GetAsync(answerId);

            if (question == null || answer == null) return false;

            var vote = new Vote { AnswerId = answerId, UserId = userId, Value = upvote ? 1 : -1 };
            await _voteRepository.AddAsync(vote);
            return true;
        }
    }
}
