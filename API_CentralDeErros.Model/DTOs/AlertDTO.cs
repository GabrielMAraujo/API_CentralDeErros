using System;
using System.ComponentModel.DataAnnotations;

namespace API_CentralDeErros.Model.DTOs
{
    public class AlertDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Level { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int NumEvents { get; set; }
        [Required]
        public bool Archived { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
