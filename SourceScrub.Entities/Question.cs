namespace SourceScrub.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Vote> Votes { get; set; }

        public Question() {
            Tags = new List<Tag>();
            Answers = new List<Answer>();
            Votes = new List<Vote>();
        }
    }
}
