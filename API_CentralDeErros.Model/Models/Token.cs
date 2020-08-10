using System;
namespace API_CentralDeErros.Model.Models
{
    public class Token
    {
        public string Secret { get; set; }
        public int ExpiresHours { get; set; }
        public string Emitter { get; set; }
        public string Address { get; set; }
    }
}
