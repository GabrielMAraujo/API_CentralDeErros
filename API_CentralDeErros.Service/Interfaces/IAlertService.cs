using API_CentralDeErros.Model;
using API_CentralDeErros.Model.DTOs;
using System;
using System.Collections.Generic;

namespace API_CentralDeErros.Service
{
    public interface IAlertService
    {
        IList<AlertDTO> GetAll(Boolean archived);
        IList<AlertDTO> SearchAlerts(int environment, int searchBy, string text);
        AlertDTO AddAlert(int userId, string level, string title, string description, string origin, int environment, string token);
        public Alert GetAlertById(int id);
        public Alert ArchiveAlert(int id);
        public bool DeleteAlert(int id);
    }
}