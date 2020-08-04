using System;
using API_CentralDeErros.Model;
using API_CentralDeErros.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_CentralDeErros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("/login")]
        public ActionResult<string> Login(string userName, string password)
        {
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

        [HttpPost("/register")]
        public ActionResult<User> Register(string email, string userName, string password)
        {
            User user = _service.AddUser(email, userName, password);

            //Se foi possível inserir, retorna OK. 
            if (user != null)
            {
                return user;
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
