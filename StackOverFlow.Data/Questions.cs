using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design; 
using StackOverFlow.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using User.Data;
using Answer.Data;


namespace Questions.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public List<Like> Likes { get; set; }
        public DateTime DatePosted { get; set; }
        public List<QuestionsTags> QuestionsTags { get; set; }
        public List<Answers> Answers { get; set; }
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
}
  