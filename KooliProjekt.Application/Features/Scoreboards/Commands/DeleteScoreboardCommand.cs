using MediatR;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Features.Scoreboards.Commands
{
    public class DeleteScoreboardCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}