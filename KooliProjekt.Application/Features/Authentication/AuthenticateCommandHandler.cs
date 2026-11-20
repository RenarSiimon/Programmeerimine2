using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Authentication;
using KooliProjekt.Application.Authentication.Jwt;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Authentication
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, OperationResult<string>>
    {
        private const string InvalidCredentialsError = "Invalid e-mail or password.";

        private readonly ApplicationDbContext _dbContext;
        private readonly JwtTokenBuilder _tokenBuilder;

        public AuthenticateCommandHandler(ApplicationDbContext dbContext, JwtTokenBuilder tokenBuilder)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _tokenBuilder = tokenBuilder ?? throw new ArgumentNullException(nameof(tokenBuilder));
        }

        public async Task<OperationResult<string>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var response = new OperationResult<string>();

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == request.UserName, cancellationToken);

            if (user == null || !PasswordHelper.Verify(request.Password, user.Password))
            {
                response.AddError(InvalidCredentialsError);
                return response;
            }

            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new System.Security.Claims.Claim(ClaimTypes.Name, user.Username)
            };

            if (user.IsAdmin)
            {
                claims.Add(new System.Security.Claims.Claim(ClaimTypes.Role, "Admin"));
            }

            var token = _tokenBuilder.GenerateToken(claims.ToArray());
            response.Value = token;

            return response;
        }
    }
}