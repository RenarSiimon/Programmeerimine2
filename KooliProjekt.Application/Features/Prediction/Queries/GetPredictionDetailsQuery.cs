using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.Application.Infrastructure.Paging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Dto;

namespace KooliProjekt.Application.Features.Prediction.Queries
{
    public class GetPredictionDetailsQuery : IRequest<OperationResult<PredictionDto>>
    {
        public int Id { get; set; }
    }

    public class GetPredictionDetailsQueryHandler : IRequestHandler<GetPredictionDetailsQuery, OperationResult<PredictionDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetPredictionDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PredictionDto>> Handle(GetPredictionDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Predictions
                .Where(p => p.Id == request.Id)
                .Select(p => new PredictionDto
                {
                    Id = p.Id
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return OperationResult<PredictionDto>.NotFound("Prediction not found.");
            }

            return OperationResult<PredictionDto>.Success(entity);
        }
    }
}