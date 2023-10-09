namespace SourceScrub.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User? User { get; set; }
        public int QuestionId { get; set; }
        public Question? Question { get; set; }
        public ICollection<Vote> Votes { get; set; }

        public Answer() {
            Votes = new List<Vote>();
        }
    }
}
