using API_CentralDeErros.Service;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using API_CentralDeErros.Model.DTOs;

namespace API_CentralDeErros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly IAlertService _service;

        public AlertController(IAlertService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
    }
}
