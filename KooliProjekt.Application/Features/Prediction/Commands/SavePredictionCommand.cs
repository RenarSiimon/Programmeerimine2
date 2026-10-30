using System;
using MediatR;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Features.Prediction.Commands
{
    public class SavePredictionCommand : IRequest<OperationResult<int>>
    {
        public int Id { get; set; }
        public DateTime PredictionTime { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public int Points { get; set; }
    }
}