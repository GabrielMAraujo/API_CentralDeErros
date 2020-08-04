using System;
using API_CentralDeErros.Model;

namespace API_CentralDeErros.Service.Interfaces
{
    public interface IUserService
    {
        public User GetUser(string userName, string password);
        public User AddUser(string email, string userName, string password);
    }
}
