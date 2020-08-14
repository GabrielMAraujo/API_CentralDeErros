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

        public enum EEnvironment
        {
            DEV = 1, HOMOLOGACAO = 2, PRODUCAO = 3
        }

        public enum ESearchBy
        {
            Level = 1, Description = 2, Origin = 3
        }

        public IList<AlertDTO> SearchAlerts(int environment, int searchBy, string text)
        {
            string env = Enum.GetName(typeof(EEnvironment), environment);
            string prop = Enum.GetName(typeof(ESearchBy), searchBy);

            var alerts = _context.Alerts
                .Where(item => item.Type.ToUpper() == env)
                .ToList();

            if (prop != null && text != null)
                for (int i = alerts.Count - 1; i >= 0; i--)
                {
                    var value = alerts[i].GetType().GetProperty(prop).GetValue(alerts[i], null);
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