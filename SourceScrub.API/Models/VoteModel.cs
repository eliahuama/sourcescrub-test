namespace SourceScrub.API.Models
{
    public class VoteModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserModel? User { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public int Value { get; set; } // -1 for downvote, 1 for upvote
    }
}
