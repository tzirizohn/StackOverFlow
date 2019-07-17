using System;
using System.Collections.Generic;
using System.Text;
using Questions.Data;
using Answer.Data;
using User.Data;

namespace StackOverFlow.Data
{
    public class ViewModel
    {
        public Question question { get; set; }
        public IEnumerable<Question> questions { get; set; }
        public IEnumerable<Answers> answers { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public bool IsLoggedIn { get; set; }
        public Users user { get; set; }
        public bool AlreadyLiked { get; set; }
    }
}
