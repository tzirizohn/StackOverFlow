using System;
using System.Collections.Generic;
using System.Text;
using StackOverFlow.Data;
using Questions.Data;
using User.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Answer.Data
{
    public class Answers
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public Question Question { get; set; }
        public Users User { get; set; }
    }

    public class AnswerRepository
    {
        private string _connectionString;

        public AnswerRepository(string connectionString)
        {
            _connectionString = connectionString;  
        }

        public IEnumerable<Answers> GetAnswers(int questionid)
        {
            using (var context = new QuestionsTagsContext(_connectionString))
            {
                return context.Answers.Include(a => a.User).Where(a => a.QuestionId == questionid).ToList();
            }
        }

        public void AddAnswer(Answers a)
        {
            using (var context = new QuestionsTagsContext(_connectionString))
            {
                context.Answers.Add(a);
                context.SaveChanges();
            }
        }
         
        
    }
}
