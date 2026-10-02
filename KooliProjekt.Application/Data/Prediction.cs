using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class Prediction
    {
        public int Id { get; set; }
        public DateTime PredictionTime { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public int Points { get; set; }

        public void CountPoints()
        {
        }

        public void Allowed()
        {
        }
    }
}