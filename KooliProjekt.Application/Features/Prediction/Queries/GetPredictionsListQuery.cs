using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Prediction.Queries
{
    public class GetPredictionsListQuery : IRequest<List<PredictionDto>>
    {
    }

    public class GetPredictionsListQueryHandler : IRequestHandler<GetPredictionsListQuery, List<PredictionDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetPredictionsListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PredictionDto>> Handle(GetPredictionsListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Predictions
                .Select(p => new PredictionDto
                {
                    Id = p.Id
                })
                .ToListAsync(cancellationToken);
        }
    }

    public class PredictionDto
    {
        public int Id { get; set; }
    }
}