using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Teams.Commands
{
    public class AddFileCommandValidator : AbstractValidator<AddFileCommand>
    {
        private const long MaximumFileSize = 5 * 1024 * 1024;

        public AddFileCommandValidator(ApplicationDbContext context)
        {
            RuleFor(command => command.TeamId)
                .GreaterThan(0)
                .MustAsync((teamId, cancellationToken) => TeamExistsAsync(context, teamId, cancellationToken))
                .WithMessage("Team not found.");

            RuleFor(command => command.File)
                .NotNull()
                .WithMessage("A file is required.")
                .Must(file => file == null || file.Length <= MaximumFileSize)
                .WithMessage("The file must be 5 MB or smaller.")
                .Must(HasFileName)
                .WithMessage("The file must have a valid name.");
        }

        private static Task<bool> TeamExistsAsync(ApplicationDbContext context, int teamId, CancellationToken cancellationToken)
        {
            return context.Teams.AnyAsync(team => team.Id == teamId, cancellationToken);
        }

        private static bool HasFileName(IFormFile file)
        {
            return file != null && !string.IsNullOrWhiteSpace(file.FileName);
        }
    }
}
