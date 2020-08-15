using API_CentralDeErros.Model.Models.JSON;
using API_CentralDeErros.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;

namespace API_CentralDeErros.API.Controllers
{
    [Authorize]
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

        // Adiciona novo alerta
        // api/alert
        [HttpPost]
        public ActionResult Add([FromBody] AlertAddJSON alert)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];

            return Ok(_service.AddAlert(alert.UserId, alert.Level, alert.Title, alert.Description, alert.Origin, alert.EnvironmentId, accessToken));
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