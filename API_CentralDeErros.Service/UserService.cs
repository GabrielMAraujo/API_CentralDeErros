using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_CentralDeErros.Infra;
using API_CentralDeErros.Model;
using API_CentralDeErros.Model.Models;
using API_CentralDeErros.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API_CentralDeErros.Service
{
    public class UserService : IUserService
    {
        private readonly CentralContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Token _token;

        public UserService(CentralContext context, UserManager<IdentityUser> userManager, IOptions<Token> token)
        {
            _context = context;
            _userManager = userManager;
            _token = token?.Value;
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

        public async Task<bool> LoginUser(string email, string password)
        {
            //Achando usuário por email
            var user = await _userManager.FindByEmailAsync(email);

            if(user != null)
            {
                //Verificando se a senha é correta
                if(await _userManager.CheckPasswordAsync(user, password))
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<IdentityResult> RegisterUser(string name, string email, string password)
        {
            //Criação do objeto de usuário
            IdentityUser user = new IdentityUser()
            {
                UserName = name,
                Email = email
            };

            //Insere no DB
            var response = await _userManager.CreateAsync(user, password);

            return response;
        }

        //Gerar o token JWT com as configurações armazenadas
        public string GenerateToken()
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_token.Secret);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _token.Emitter,
                Audience = _token.Address,
                Expires = DateTime.UtcNow.AddHours(_token.ExpiresHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<string> GenerateResetPasswordToken(string email)
        {
            //Achar se email é correspondente no DB
            IdentityUser user = await _userManager.FindByEmailAsync(email);

            //Se não achar, retornar exceção para tratamento na controller
            if(user == null)
            {
                throw new MissingMemberException();
            }

            //Gerando o token para resetar a senha
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            return resetToken;
        }

        //Reseta a senha do usuário
        public async Task<bool> ResetPassword(string email, string newPassword, string token)
        {
            //Achar se email é correspondente no DB
            IdentityUser user = await _userManager.FindByEmailAsync(email);

            IdentityResult result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result.Succeeded;
        }
    }
}