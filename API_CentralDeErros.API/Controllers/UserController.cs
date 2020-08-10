using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_CentralDeErros.Model;
using API_CentralDeErros.Model.Models.JSON;
using API_CentralDeErros.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_CentralDeErros.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

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
    }
}
