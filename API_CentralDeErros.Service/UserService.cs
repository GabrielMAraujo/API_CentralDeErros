using System;
using System.Linq;
using API_CentralDeErros.Infra;
using API_CentralDeErros.Model;
using API_CentralDeErros.Service.Interfaces;

namespace API_CentralDeErros.Service
{
    public class UserService : IUserService
    {
        private readonly CentralContext _context;

        public UserService(CentralContext context)
        {
            _context = context;
        }

        public User GetUser(string userName, string password)
        {
            return _context.Users.First(u => u.UserName == userName && u.Password == password);
        }

        public User AddUser(string email, string userName, string password)
        {
            //Criando usuário a ser inserido
            User user = new User(email, userName, password);

            return _context.Add(user).Entity;
        }
    }
}