using API_CentralDeErros.Infra;
using API_CentralDeErros.Model.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace API_CentralDeErros.Service
{
    public class AlertService : IAlertService
    {
        private readonly CentralContext _context;
        private readonly IMapper _mapper;

        public AlertService(CentralContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IList<AlertDTO> GetAll()
        {
            var alerts = _context.Alerts
                .Where(item => item.Archived == false)
                .ToList();

            return _mapper.Map<IList<AlertDTO>>(alerts);
        }
    }
}
