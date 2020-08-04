using API_CentralDeErros.Model;
using System.Collections.Generic;

namespace API_CentralDeErros.Service
{
    public interface IAlertService
    {
        IList<Alert> GetAll();
    }
}
