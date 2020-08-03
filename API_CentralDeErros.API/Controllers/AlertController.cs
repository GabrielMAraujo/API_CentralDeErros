using API_CentralDeErros.Service;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using API_CentralDeErros.Model.DTOs;
using AutoMapper;

namespace API_CentralDeErros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly IAlertService _service;
        private readonly IMapper _mapper;

        public AlertController(IAlertService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IList<AlertDTO>> GetAll()
        {
            return Ok(_mapper.Map<IList<AlertDTO>>(_service.GetAll()));
        }
    }
}
