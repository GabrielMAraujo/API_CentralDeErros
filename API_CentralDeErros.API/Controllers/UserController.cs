using System;
using API_CentralDeErros.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_CentralDeErros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController()
        {
        }

        [HttpPost]
        public ActionResult<string> Login(string userName, string password)
        {
            //Placeholder pro token no futuro
            string token = "";

            return Ok(token);
        }

        [HttpPost]
        public ActionResult<string> Register(string userName, string password)
        {
            //Placeholder pro token no futuro
            string token = "";

            return Ok(token);
        }
    }
}
