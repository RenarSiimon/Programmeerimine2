using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Prediction.Commands
{
    public class SavePredictionCommandHandler : IRequestHandler<SavePredictionCommand, OperationResult<int>>
    {
        private readonly ApplicationDbContext _context;

        public SavePredictionCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<int>> Handle(SavePredictionCommand request, CancellationToken cancellationToken)
        {
            Data.Prediction prediction;

            if (request.Id == 0)
            {
                prediction = new Data.Prediction();
                _context.Predictions.Add(prediction);
            }
            else
            {
                prediction = await _context.Predictions.FindAsync(new object[] { request.Id }, cancellationToken);
                if (prediction == null)
                {
                    return OperationResult<int>.NotFound("Prediction not found");
                }
            }

            prediction.PredictionTime = request.PredictionTime;
            prediction.HomeTeamGoals = request.HomeTeamGoals;
            prediction.AwayTeamGoals = request.AwayTeamGoals;
            prediction.Points = request.Points;

            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult<int>.Success(prediction.Id);
        }
    }
}