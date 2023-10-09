namespace SourceScrub.API.Models
{
    public class AnswerModel
    {
        public int? Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int UserId { get; set; }
        public UserModel? User { get; set; }
        public int QuestionId { get; set; }
        public ICollection<VoteModel>? Votes { get; set; }
    }
}
