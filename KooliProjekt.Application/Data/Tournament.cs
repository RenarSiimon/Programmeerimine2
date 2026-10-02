using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void AddGame()
        {
        }

        public void LookScoreboard()
        {
        }
    }
}