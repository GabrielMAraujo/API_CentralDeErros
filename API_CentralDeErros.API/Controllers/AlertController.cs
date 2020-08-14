using API_CentralDeErros.Model.Models.JSON;
using API_CentralDeErros.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_CentralDeErros.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly IAlertService _service;

        public AlertController(IAlertService service)
        {
            _service = service;
        }

        // Retorna todas os alertas não arquivados
        // api/alert
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        // Buscar alertas filtrando por ambiente e de acordo com um campo do alerta de busca
        // api/alert/{ambiente}/{campo}?text=texto que será pesquisado
        [HttpGet("search/{environment?}/{searchBy?}")]
        public ActionResult Get(int environment, int searchBy, [FromQuery(Name = "text")] string text)
        {
            return Ok(_service.SearchAlerts(environment, searchBy, text));
        }
    }
}