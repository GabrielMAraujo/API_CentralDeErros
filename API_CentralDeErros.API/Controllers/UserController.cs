using System;
using System.Collections.Generic;
using API_CentralDeErros.Model;
using API_CentralDeErros.Model.Models;
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
        public ActionResult<string> Login(string userName, string password)
        {
            Console.WriteLine("asdasdsad");

            User user = _service.GetUser(userName, password);

            //Se houver login no DB, retornar OK. Se não, retornar não autorizado
            if (user != null)
            {
                return Ok(user.Token);
            }
            else
            {
                return StatusCode(401);
            }
        }

        [HttpPost("register")]
        public ActionResult<User> Register([FromBody] UserJSON user)
        {
            User newUser = _service.AddUser(user.Email, user.UserName, user.Password);

            //Se foi possível inserir, retorna OK. 
            if (newUser != null)
            {
                return newUser;
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
