using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverFlow.Models;   
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration; 
using StackOverFlow.Data;
using Microsoft.AspNet.Identity;
using Questions.Data;
using User.Data;
using Answer.Data;

namespace StackOverFlow.Controllers
{
    public class HomeController : Controller
    {                      
        private IHostingEnvironment _environment;
        private string _connectionString;               

        public HomeController(IHostingEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _connectionString = configuration.GetConnectionString("ConStr");
        }                                                                         

        public IActionResult Index()
        {
            QuestionRepository qt = new QuestionRepository(_connectionString);
            IEnumerable<Question> questions = qt.GetQuestions(); 
            return View(questions);
        }

        [Authorize]
        public IActionResult QuestionForm()
        {
            return View();
        }

        public IActionResult AddQuestion(Question question, IEnumerable<string> tags)
        {
            QuestionRepository qr = new QuestionRepository(_connectionString);
            qr.AddQuestion(question, tags);
            return Redirect("/");
        }    

        public IActionResult ViewQuestion(int id)
        {
            QuestionRepository qr = new QuestionRepository(_connectionString);       
            UserRepository ur = new UserRepository(_connectionString);
            AnswerRepository ar = new AnswerRepository(_connectionString); 
            ViewModel vm = new ViewModel();                            
            vm.question = qr.GetQuestion(id);
            vm.Tags = qr.GetTagsForQuestion(vm.question.QuestionsTags);    
            if (User.Identity.IsAuthenticated)
            {
                vm.user = ur.GetByEmail(User.Identity.Name);
                vm.AlreadyLiked = qr.AlreadyLiked(vm.user.id, vm.question.Id);  
                vm.IsLoggedIn = true;  
            }

            vm.answers = ar.GetAnswers(id);
            return View(vm);
        }

        public IActionResult Answer(Answers answer, int questionid, int userid)
        {
            AnswerRepository ar = new AnswerRepository(_connectionString);
            answer.QuestionId = questionid;      
            answer.UserId = userid;
            answer.Text = answer.Text;
            ar.AddAnswer(answer);
            return Redirect($"/home/viewquestion?id={answer.QuestionId}");
        }

        public IActionResult AddLike(Like like)
        {
            QuestionRepository qr = new QuestionRepository(_connectionString);
            qr.AddLike(like);
            return Redirect($"/home/viewquestion?id={like.QuestionId}");
        }
          
    }         
}
