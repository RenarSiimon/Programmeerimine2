using MediatR;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Features.Tournaments.Commands
{
    public class DeleteTournamentCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}