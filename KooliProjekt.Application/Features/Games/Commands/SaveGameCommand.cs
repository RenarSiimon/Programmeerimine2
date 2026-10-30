using System;
using MediatR;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Features.Games.Commands
{
    public class SaveGameCommand : IRequest<OperationResult<int>>
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
    }
}