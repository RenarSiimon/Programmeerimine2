using MediatR;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Features.Users.Commands
{
    public class SaveUserCommand : IRequest<OperationResult<int>>
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}