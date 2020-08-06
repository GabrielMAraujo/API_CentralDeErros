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

        public User GetUser(string email, string password)
        {
            Console.WriteLine(_context.Users.ToList().Count);

            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public User AddUser(string email, string userName, string password)
        {
            //Criando usuário a ser inserido
            User user = new User(email, userName, password);

            var ret = _context.Add(user).Entity;

            _context.SaveChanges();

            return ret;
        }
    }
}