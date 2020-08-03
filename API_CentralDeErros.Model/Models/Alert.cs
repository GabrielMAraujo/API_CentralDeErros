using System;
using System.ComponentModel.DataAnnotations;

namespace API_CentralDeErros.Model
{
    public class Alert
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(7)]
        public string Level { get; set; }
        [Required, MaxLength(150)]
        public string Title { get; set; }
        [Required, MaxLength(300)]
        public string Description { get; set; }
        [Required, MaxLength(15)]
        public string Origin { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required, MaxLength(11)]
        public string Type { get; set; }
        [Required]
        public int NumEvents { get; set; }
        [Required]
        public bool Archived { get; set; }
        [Required, MaxLength(50)]
        public string Token { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
