using MediatR;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Prediction.Commands
{
    public class DeletePredictionCommandHandler : IRequestHandler<DeletePredictionCommand, OperationResult>
    {
        private readonly ApplicationDbContext _context;

        public DeletePredictionCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> Handle(DeletePredictionCommand request, CancellationToken cancellationToken)
        {
            var prediction = await _context.Predictions.FindAsync(new object[] { request.Id }, cancellationToken);
            if (prediction == null)
            {
                return OperationResult.NotFound("Prediction not found");
            }

            _context.Predictions.Remove(prediction);
            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}