using MediatR;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Features.Teams.Commands
{
    public class DeleteTeamCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}