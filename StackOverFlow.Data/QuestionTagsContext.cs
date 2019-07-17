using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Questions.Data;
using User.Data;
using Answer.Data;
using System;
using System.IO;

namespace StackOverFlow.Data
{
    public class QuestionsTagsContext : DbContext
    {
        private string _connectionString;

        public QuestionsTagsContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Users> User { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Tag> Tags { get; set; }  
        public DbSet<Answers> Answers { get; set; }
        public DbSet<QuestionsTags> QuestionTags { get; set; }
        public DbSet<Like> Like { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionsTags>().HasKey(qt => new { qt.QuestionId, qt.TagId });

            modelBuilder.Entity<QuestionsTags>()
                .HasOne<Question>(q => q.Question)
                .WithMany(q => q.QuestionsTags)
                .HasForeignKey(qt => qt.QuestionId);


            modelBuilder.Entity<QuestionsTags>()
                .HasOne<Tag>(t => t.Tag)
                .WithMany(t => t.QuestionsTags)
                .HasForeignKey(t => t.TagId);
        }
    }

    public class QuestionsTagsContextFactory : IDesignTimeDbContextFactory<QuestionsTagsContext>
    {
        public QuestionsTagsContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}StackOverFlow"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new QuestionsTagsContext(config.GetConnectionString("ConStr"));
        }
    }
}
