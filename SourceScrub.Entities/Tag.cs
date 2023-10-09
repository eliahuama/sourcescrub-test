namespace SourceScrub.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public ICollection<Question> Questions { get; set; }

        public Tag()
        {
            Questions = new List<Question>();
        }
    }
}
