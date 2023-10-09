namespace SourceScrub.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public ICollection<Question> Questions { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Vote> Votes { get; set; }

        public User()
        {
            Questions = new List<Question>();
            Answers = new List<Answer>();
            Votes = new List<Vote>();
        }
    }
}
