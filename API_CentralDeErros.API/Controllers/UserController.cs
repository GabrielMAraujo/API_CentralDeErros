using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_CentralDeErros.Model;
using API_CentralDeErros.Model.Models.JSON;
using API_CentralDeErros.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_CentralDeErros.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly ILogger _logger;

        public UserController(IUserService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Faz o login do usuário com email e senha.
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginJSON user)
        {
            //User foundUser = _service.GetUser(user.Email, user.Password);

            ////Se houver login no DB, retornar OK. Se não, retornar não autorizado
            //if (foundUser != null)
            //{
            //    //return Ok(foundUser.Token);

            //    //Provisório
            //    return Ok(foundUser.UserName);
            //}
            //else
            //{
            //    return StatusCode(401);
            //}

            bool logged = await _service.LoginUser(user.Email, user.Password);

            if (logged)
            {
                return Ok(_service.GenerateToken());
            }
            else
            {
                return StatusCode(401, "Usuário ou senha incorretos");
            }
        }

        /// <summary>
        /// Registra o usuário com nome, email e senha.
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] UserRegisterJSON user)
        {
            //User newUser = _service.AddUser(user.Email, user.UserName, user.Password);

            ////Se foi possível inserir, retorna OK. 
            //if (newUser != null)
            //{
            //    //Provisório
            //    return newUser;
            //}
            //else
            //{
            //    return StatusCode(500);
            //}

            var response = await _service.RegisterUser(user.UserName, user.Email, user.Password);

            if (response.Succeeded)
            {
                return Ok(_service.GenerateToken());
            }
            else
            {
                return StatusCode(500, response.Errors);
            }
        }

        /// <summary>
        /// Gera um token para redefinir a senha de um usuário a partir de seu email.
        /// </summary>
        [HttpPost("forgotpassword")]
        public async Task<ActionResult<string>> ForgotPassword([FromBody] UserResetJSON user)
        {
            string token = "";

            try
            {
                 token = await _service.GenerateResetPasswordToken(user.Email);
            }

            catch(Exception e)
            {
                if(e.GetType().Name == "MissingMemberException")
                {
                    return StatusCode(401);
                }
                else
                {
                    _logger.LogError("Erro na geração do token: " + e);
                    return StatusCode(500);
                }
            }

            return Ok(token);
        }

        /// <summary>
        /// Redefine a senha de um usuário a partir do token recebido.
        /// </summary>
        [HttpPost("resetpassword")]
        public async Task<ActionResult> ResetPassword([FromBody] NewPasswordJSON json)
        {
            bool success = false;

            try
            {
                success = await _service.ResetPassword(json.Email, json.NewPassword, json.Token);
            }
            catch(Exception e)
            {
                _logger.LogError("Erro ao redefinir senha do usuário: " + e);
                return StatusCode(500, e);
            }

            if (success)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "Erro do servidor");
            }
        }

    }
}
