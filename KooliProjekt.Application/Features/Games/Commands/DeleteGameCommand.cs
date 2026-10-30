using MediatR;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Features.Games.Commands
{
    public class DeleteGameCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}