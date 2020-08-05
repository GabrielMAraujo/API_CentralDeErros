using API_CentralDeErros.Infra;
using API_CentralDeErros.Model;
using API_CentralDeErros.Model.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public IList<AlertDTO> SearchAlerts(string environment, string searchBy, string text)
        {
            var alerts = _context.Alerts
                .Where(item => item.Type.ToUpper() == environment)
                .ToList();

            for (int i = alerts.Count - 1; i >= 0; i--)
            {
                var value = alerts[i].GetType().GetProperty(searchBy).GetValue(alerts[i], null);
                if (value.ToString().Contains(text) == false)
                    alerts.RemoveAt(i);
            }

            return _mapper.Map<IList<AlertDTO>>(alerts);
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
