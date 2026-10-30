using MediatR;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Features.Tournaments.Commands
{
    public class SaveTournamentCommand : IRequest<OperationResult<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}