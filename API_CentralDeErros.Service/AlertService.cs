using API_CentralDeErros.Infra;
using API_CentralDeErros.Model;
using API_CentralDeErros.Model.DTOs;
using AutoMapper;
using System;
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

        public enum EEnvironment
        {
            DESENVOLVIMENTO = 1, HOMOLOGAÇÃO = 2, PRODUÇÃO = 3
        }

        public enum ESearchBy
        {
            Level = 1, Description = 2, Origin = 3
        }

        public IList<AlertDTO> SearchAlerts(int environment, int searchBy, string text)
        {
            string env = Enum.GetName(typeof(EEnvironment), environment);
            string prop = Enum.GetName(typeof(ESearchBy), searchBy);

            var alerts = _context.Alerts.ToList();

            if (environment != 0)
                alerts = _context.Alerts
                    .Where(item => item.Type == env)
                    .ToList();

            if (searchBy == 0 && text != null)
                alerts = alerts
                    .Where(a => a.Description.Contains(text) || a.Level.Contains(text) || a.Origin.Contains(text))
                    .ToList();
            else if (prop != null && text != null)
                for (int i = alerts.Count - 1; i >= 0; i--)
                {
                    var value = alerts[i].GetType().GetProperty(prop).GetValue(alerts[i], null);
                    if (value.ToString().Contains(text) == false)
                        alerts.RemoveAt(i);
                }

            return _mapper.Map<IList<AlertDTO>>(alerts);
        }

        public IList<AlertDTO> GetAll(Boolean archived)
        {
            var alerts = _context.Alerts
                .Where(item => item.Archived == archived)
                .ToList();

            return _mapper.Map<IList<AlertDTO>>(alerts);
        }

        public AlertDTO AddAlert(int userId, string level, string title, string description, string origin, int environment, string token)
        {
            string env = Enum.GetName(typeof(EEnvironment), environment);
            DateTime date = DateTime.Now;

            Alert alert = new Alert(userId, level, title, description, origin, env, token, date);

            var newAlert = _context.Add(alert).Entity;
            _context.SaveChanges();

            return _mapper.Map<AlertDTO>(newAlert);
        }
    }
}