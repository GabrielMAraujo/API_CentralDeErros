using API_CentralDeErros.Model.DTOs;
using System.Collections.Generic;

namespace API_CentralDeErros.Service
{
    public interface IAlertService
    {
        IList<AlertDTO> GetAll();
    }
}
