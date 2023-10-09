using Microsoft.EntityFrameworkCore;
using SourceScrub.Entities;

namespace SourceScrub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasOne(q => q.User)
                .WithMany(u => u.Questions)
                .HasForeignKey(q => q.UserId);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Votes)
                .WithOne(v => v.Question)
                .HasForeignKey(v => v.QuestionId);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Tags)
                .WithMany(t => t.Questions);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Votes)
                .WithOne(a => a.Question);

            modelBuilder.Entity<Answer>()
                .HasMany(q => q.Votes)
                .WithOne(v => v.Answer)
                .HasForeignKey(v => v.AnswerId);
        }
    }
}
