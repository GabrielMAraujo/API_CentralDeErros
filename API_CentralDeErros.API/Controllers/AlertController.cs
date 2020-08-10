using API_CentralDeErros.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public enum EEnvironment
        {
            DEV = 1, HOMOLOGACAO = 2, PRODUCAO = 3
        }

        public enum ESearchBy
        {
            Level = 1, Description = 2, Origin = 3
        }

        // Buscar alertas filtrando por ambiente e de acordo com um campo do alerta de busca
        // api/alert/{ambiente}/{campo}?text=texto que será pesquisado
        [HttpGet("search/{environment?}/{searchBy?}")]
        public ActionResult Get(EEnvironment environment, ESearchBy searchBy, [FromQuery(Name = "text")] string text)
        {
            return Ok(_service.SearchAlerts(
                Enum.GetName(typeof(EEnvironment), environment),
                Enum.GetName(typeof(ESearchBy), searchBy),
                text));
        }
    }
}
