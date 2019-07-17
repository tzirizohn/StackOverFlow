using System;
using System.Collections.Generic;
using System.Text;
using User.Data;
using Questions.Data;
using StackOverFlow.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace StackOverFlow.Data
{
    public class Like
    {   
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public Users User { get; set; }
        public Question Question { get; set; }
    }    
}
