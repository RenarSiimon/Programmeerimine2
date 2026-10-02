using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class Game
    {
        public int Id { get; set; }
        public Round Round { get; set; }
        public DateTime StartTime { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }

        public void Started()
        {
        }

        public void TeamsConfirmed()
        {
        }

        public void Happened()
        {
        }
    }
}