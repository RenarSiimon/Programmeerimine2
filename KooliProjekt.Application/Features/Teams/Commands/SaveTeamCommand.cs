using MediatR;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Features.Teams.Commands
{
    public class SaveTeamCommand : IRequest<OperationResult<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}