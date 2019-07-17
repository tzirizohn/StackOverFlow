using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using User.Data;

namespace StackOverFlow.Controllers
{
    public class UserController : Controller
    {
        private IHostingEnvironment _environment;
        private string _connectionString;

        public UserController(IHostingEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _connectionString = configuration.GetConnectionString("ConStr");
        }   

        public IActionResult SignUpForm()
        {
            return View();
        }

        public IActionResult SignUp(Users user)
        {
            UserRepository ur = new UserRepository(_connectionString);
            ur.AddUser(user);
            return Redirect("/user/loginform");
        }

        public IActionResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            UserRepository ur = new UserRepository(_connectionString);

            bool isVerified = ur.Login(Email, Password);
            if (isVerified)
            {
                var claims = new List<Claim>
                {
                    new Claim("user", Email)
                };
                HttpContext.SignInAsync(new ClaimsPrincipal(
                    new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();

                return Redirect("/home/index");
            }
            else
            {
                return Redirect("/user/loginform");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/user/loginform");
        }
    }
}