using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SourceScrub.API.Models;
using SourceScrub.Business.Services.Interfaces;
using SourceScrub.Entities;
using System.Text.Json;

namespace SourceScrub.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public QuestionsController(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves questions by tags or all.
        /// </summary>
        /// <param name="tags">The tags to filter questions by.</param>
        /// <returns>A list of questions matching the tags.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionModel>>> Get([FromQuery] List<string>? tags)
        {
            var questions = await (tags?.Any() == true
                ? _questionService.GetByTags(tags)
                : _questionService.GetAll()).ToListAsync();
            return Ok(_mapper.Map<List<QuestionModel>>(questions));
        }

        /// <summary>
        /// Retrieves a specific question by ID.
        /// </summary>
        /// <param name="id">The ID of the question.</param>
        /// <returns>The question details.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionModel>> Get(int id)
        {
            var question = await _questionService.GetAsync(id);
            if (question == null) return NotFound();
            return Ok(_mapper.Map<QuestionModel>(question));
        }

        /// <summary>
        /// Adds a new question.
        /// </summary>
        /// <param name="questionModel">The question to be added.</param>
        /// <returns>The newly created question.</returns>
        [HttpPost]
        public async Task<ActionResult<QuestionModel>> Post([FromBody]QuestionModel questionModel)
        {
            if(questionModel == null) return BadRequest();
            var question = _mapper.Map<Question>(questionModel);
            question = await _questionService.AddAsync(question);
            return CreatedAtAction(nameof(Get), new { id = question.Id }, _mapper.Map<QuestionModel>(question));
        }

        /// <summary>
        /// Adds an answer to an existing question.
        /// </summary>
        /// <param name="questionId">The ID of the question.</param>
        /// <param name="answerModel">The answer to be added.</param>
        /// <returns>The newly created answer.</returns>
        [HttpPost("{questionId}/answers")]
        public async Task<ActionResult<AnswerModel>> PostAnswer(int questionId, AnswerModel answerModel)
        {
            var answer = _mapper.Map<Answer>(answerModel);
            if(answer == null) return BadRequest();
            answer.QuestionId = questionId;
            var added = await _questionService.AddAnswerAsync(answer);
            if (added == null) return NotFound();
            return CreatedAtAction(nameof(Get), new { id = questionId }, _mapper.Map<AnswerModel>(added));
        }

        /// <summary>
        /// Casts a vote on a specific question.
        /// </summary>
        /// <param name="questionId">The ID of the question to vote on.</param>
        /// <param name="userId">The ID of the voting user.</param>
        /// <param name="upvote">True for upvote, False for downvote.</param>
        /// <returns>A boolean indicating the success of the operation.</returns>
        [HttpPost("{questionId}/vote")]
        public async Task<ActionResult> Vote(int questionId, int userId, bool upvote)
        {
            return await _questionService.VoteAsync(questionId, userId, upvote)
                ? Ok()
                : NotFound();
        }

        /// <summary>
        /// Casts a vote on a specific answer to a question.
        /// </summary>
        /// <param name="questionId">The ID of the question the answer belongs to.</param>
        /// <param name="answerId">The ID of the answer to vote on.</param>
        /// <param name="upvote">True for upvote, False for downvote.</param>
        /// <returns>A boolean indicating the success of the operation.</returns>
        [HttpPost("{questionId}/answers/{answerId}/votes")]
        public async Task<ActionResult> VoteOnAnswer(int questionId, int answerId, int userId, bool upvote)
        {
            return await _questionService.VoteAsync(questionId, answerId, userId, upvote)
                ? Ok()
                : NotFound();
        }

        [HttpPost("bulk-upload")]
        public async Task<ActionResult> BulkUpload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");

            using var stream = new StreamReader(file.OpenReadStream());
            var jsonContent = await stream.ReadToEndAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var models = JsonSerializer.Deserialize<List<QuestionModel>>(jsonContent, options);
            if (models == null || !models.Any()) return BadRequest("Invalid file content");
            var questions = _mapper.Map<List<Question>>(models);
            await _questionService.AddAsync(questions);

            return Ok("Bulk insert successful");
        }
    }
}
