using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Prediction.Queries
{
    public class GetPredictionDetailsQuery : IRequest<PredictionDto>
    {
        public int Id { get; set; }
    }

    public class GetPredictionDetailsQueryHandler : IRequestHandler<GetPredictionDetailsQuery, PredictionDto>
    {
        private readonly ApplicationDbContext _context;

        public GetPredictionDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PredictionDto> Handle(GetPredictionDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Predictions
                .Where(p => p.Id == request.Id)
                .Select(p => new PredictionDto
                {
                    Id = p.Id
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}