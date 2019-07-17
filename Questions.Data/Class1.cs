using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Questions.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DatePosted { get; set; }
        public List<QuestionsTags> QuestionsTags { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuestionsTags> QuestionsTags { get; set; }
    }

    public class QuestionsTags
    {
        public int QuestionId { get; set; }
        public int TagId { get; set; }
        public Question Question { get; set; }
        public Tag Tag { get; set; }
    }

    public class QuestionViewModel
    {
        public IEnumerable<Question> questions { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }

    public class QuestionRepository
    {  
        private string _connectionString;

        public QuestionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Tag GetTag(string name)
        {
            using (var context = new QuestionsTagsContext(_connectionString))
            {
                return context.Tags.FirstOrDefault(t => t.Name == name);
            }
        }

        private int AddTag(string name)
        {
            using (var context = new QuestionsTagsContext(_connectionString))
            {
                var tag = new Tag { Name = name };
                context.Tags.Add(tag);
                context.SaveChanges();
                return tag.Id;
            }
        }

        public void AddQuestion(Question q, IEnumerable<string>tags)
        {
            using (var context = new QuestionsTagsContext(_connectionString))
            {
                context.Question.Add(q);
                foreach(string tag in tags)
                {
                    Tag t = GetTag(tag);
                    int tagId;
                    if (t == null)
                    {
                        tagId = AddTag(tag);
                    }
                    else
                    {
                        tagId = t.Id;
                    }
                    context.QuestionTags.Add(new QuestionsTags
                    {
                        QuestionId = q.Id,
                        TagId = tagId
                    });
                }
                context.SaveChanges();
            }
        }

        public IEnumerable<Question> GetQuestions()
        {
            using (var context = new QuestionsTagsContext(_connectionString))
            {
                return context.Question.ToList().OrderByDescending(q => q.DatePosted);
            }
        }
    }









    
}
