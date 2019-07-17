using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using StackOverFlow.Data;

namespace User.Data
{
    public class Users
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserRepository
    {
        private string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(Users u)
        {
            using (var context = new QuestionsTagsContext(_connectionString))
            {
                u.Password = BCrypt.Net.BCrypt.HashPassword(u.Password);
                context.User.Add(u);
                context.SaveChanges();
            }
        }

        public Users GetByEmail(string email)
        {
            using (var context = new QuestionsTagsContext(_connectionString))
            {
                return context.User.FirstOrDefault(u => u.Email == email); 
            }
        }

        public bool Login(string email, string password)
        {
            using (var context = new QuestionsTagsContext(_connectionString))
            {
                var user = GetByEmail(email);
                bool verify = BCrypt.Net.BCrypt.Verify(password, user.Password);
                return verify;
            }
        }
    }
    
}
