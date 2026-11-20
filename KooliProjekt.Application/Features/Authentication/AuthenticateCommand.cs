using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Authentication
{
    public class AuthenticateCommand : IRequest<OperationResult<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
