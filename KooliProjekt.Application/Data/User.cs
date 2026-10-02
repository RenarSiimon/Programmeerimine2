using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public void Prediction()
        {
        }

        public void Scoreboard()
        {
        }
    }
}