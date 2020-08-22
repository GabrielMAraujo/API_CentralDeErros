using API_CentralDeErros.Model.Models.JSON;
using API_CentralDeErros.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
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

        /// <summary>
        /// Retorna todas os alertas não arquivados ou arquivados.
        /// </summary>
        // api/alert
        [HttpGet]
        public ActionResult GetAll([FromQuery(Name = "archived")] Boolean archived)
        {
            return Ok(_service.GetAll(archived));
        }

        /// <summary>
        /// Adiciona novo alerta.
        /// </summary>
        // api/alert
        [HttpPost]
        public ActionResult Add([FromBody] AlertAddJSON alert)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];

            return Ok(_service.AddAlert(
                alert.UserId,
                alert.Level,
                alert.Title,
                alert.Description,
                alert.Origin,
                alert.EnvironmentId,
                accessToken)
            );
        }

        /// <summary>
        /// Buscar alertas filtrando por ambiente e de acordo com um campo do alerta de busca.
        /// </summary>
        /// <param name="environment">
        /// 0 = Todos, 1 = DESENVOLVIMENTO, 2 = HOMOLOGAÇÃO, 3 = PRODUÇÃO
        /// </param>
        /// <param name="searchBy">
        /// 0 = Todos os campos, 1 = Level, 2 = Descrição, 3 = Origem
        /// </param>
        // api/alert/{ambiente}/{campo}?text=texto que será pesquisado
        [HttpGet("search/{environment?}/{searchBy?}")]
        public ActionResult Get(int environment, int searchBy, [FromQuery(Name = "text")] string text)
        {
            return Ok(_service.SearchAlerts(environment, searchBy, text));
        }
    }
}