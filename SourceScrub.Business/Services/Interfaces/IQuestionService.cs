using SourceScrub.Entities;

namespace SourceScrub.Business.Services.Interfaces
{
    public interface IQuestionService
    {
        IQueryable<Question> GetAll();
        IQueryable<Question> GetByTags(ICollection<string> tags);
        Task<Question?> GetAsync(int id);
        Task<Question> AddAsync(Question question);
        Task<ICollection<Question>> AddAsync(ICollection<Question> question);
        Task<Answer?> AddAnswerAsync(Answer answer);
        Task<bool> VoteAsync(int questionId, int userId, bool upvote);
        Task<bool> VoteAsync(int questionId, int answerId, int userId, bool upvote);
    }
}
