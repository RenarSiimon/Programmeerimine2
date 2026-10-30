using MediatR;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Features.Prediction.Commands
{
    public class DeletePredictionCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}