using API_CentralDeErros.Infra;
using API_CentralDeErros.Model;
using System.Collections.Generic;
using System.Linq;

namespace API_CentralDeErros.Service
{
    public class AlertService : IAlertService
    {
        private readonly CentralContext _context;

        public AlertService(CentralContext context)
        {
            _context = context;
        }

        public IList<Alert> GetAll()
        {
            return _context.Alerts
                .Where(item => item.Archived == false)
                .ToList();
        }
    }
}
