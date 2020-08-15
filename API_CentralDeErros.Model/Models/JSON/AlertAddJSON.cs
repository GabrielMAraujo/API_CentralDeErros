using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_CentralDeErros.Model.Models.JSON
{
    public class AlertAddJSON
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int EnvironmentId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Level { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Origin { get; set; }
    }
}