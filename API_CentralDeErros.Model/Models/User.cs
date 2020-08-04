using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API_CentralDeErros.Model
{
    public class User
    {
        public User(string email, string userName, string password)
        {
            Email = email;
            UserName = userName;
            Password = password;
        }

        [Required]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Email { get; set; }
        [Required, MaxLength(50)]
        public string UserName { get; set; }
        [Required, MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string Token { get; set; }

        public List<Alert> Alerts { get; set; }
    }
}
