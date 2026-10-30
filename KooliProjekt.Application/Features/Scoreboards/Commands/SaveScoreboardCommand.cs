using MediatR;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Features.Scoreboards.Commands
{
    public class SaveScoreboardCommand : IRequest<OperationResult<int>>
    {
        public int Id { get; set; }
        public int Place { get; set; }
        public int Points { get; set; }
    }
}