using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Questions.Data;
using StackOverFlow.Data;

namespace StackOverFlow.Data
{
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

        public void AddQuestion(Question q, IEnumerable<string> tags)
        {
            using (var context = new QuestionsTagsContext(_connectionString))
            {
                context.Question.Add(q);
                foreach (string tag in tags)
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
                return context.Question
                     .Include(q => q.Answers)
                     .Include(q => q.Likes)
                     .Include(q => q.QuestionsTags)
                     .ThenInclude(q => q.Tag)
                     .OrderByDescending(q => q.DatePosted)
                     .ToList();
            }
        }

        public Question GetQuestion(int id)
        {
            using (var context = new QuestionsTagsContext(_connectionString))
            {
                return context.Question.Include(t => t.QuestionsTags).FirstOrDefault(q => q.Id == id);
            }
        }

        public IEnumerable<Tag> GetTagsForQuestion(IEnumerable<QuestionsTags> ids)
        {
            using (var context = new QuestionsTagsContext(_connectionString))
            {
                List<Tag> tags = new List<Tag>();
                foreach (QuestionsTags qt in ids)
                {
                    tags.Add(context.Tags.FirstOrDefault(t => t.Id == qt.TagId));
                }
                return tags;
            }
        }

        public void AddLike(Like like)
        {
            using (var contex = new QuestionsTagsContext(_connectionString))
            {
                contex.Like.Add(new Like
                {
                    QuestionId = like.QuestionId,
                    UserId = like.UserId
                });
                contex.SaveChanges();
            }
        }

        public int GetQuestionLikes(int questionId)
        {
            using (var context = new QuestionsTagsContext(_connectionString))
            {
                return context.Like.Count(q => q.QuestionId == questionId);
            }
        }

        public bool AlreadyLiked(int userId, int questionId)
        {
            using (var context = new QuestionsTagsContext(_connectionString))
            {
                Question question = context.Question.Include(l => l.Likes).FirstOrDefault(q => q.Id == questionId);
                return question.Likes.Any(u => u.UserId == userId);
            }
        }
    }
}
