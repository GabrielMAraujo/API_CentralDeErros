using System;
using System.Threading.Tasks;
using API_CentralDeErros.Model;
using Microsoft.AspNetCore.Identity;

namespace API_CentralDeErros.Service.Interfaces
{
    public interface IUserService
    {
        public User GetUser(string userName, string password);
        public User AddUser(string email, string userName, string password);

        //Métodos usando Identity
        public Task<bool> LoginUser(string email, string password);
        public Task<IdentityResult> RegisterUser(string name, string email, string password);
        public Task<string> GenerateResetPasswordToken(string email);
        public Task<bool> ResetPassword(string email, string newPassword, string token);
        //JWT
        public string GenerateToken();
    }
}
