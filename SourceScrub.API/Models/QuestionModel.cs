namespace SourceScrub.API.Models
{
    public class QuestionModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public UserModel? User { get; set; }
        public string Text { get; set; } = string.Empty;
        public ICollection<TagModel>? Tags { get; set; }
        public ICollection<AnswerModel>? Answers { get; set; }
        public ICollection<VoteModel>? Votes { get; set; }
    }
}
