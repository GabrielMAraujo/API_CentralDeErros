using System;
using System.ComponentModel.DataAnnotations;

namespace API_CentralDeErros.Model
{
    public class Alert
    {
        public Alert(int userId, string level, string title, string description, string origin, string type, string token, DateTime date)
        {
            UserId = userId;
            Level = level;
            Title = title;
            Description = description;
            Origin = origin;
            Type = type;
            Date = date;
            Token = token;
        }

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
        [Required, MaxLength(15)]
        public string Type { get; set; }
        [Required]
        public int NumEvents { get; set; }
        [Required]
        public bool Archived { get; set; }
        [Required, MaxLength(300)]
        public string Token { get; set; }

        [Required]
        public int UserId { get; set; }

        //public User User { get; set; }
    }
}
