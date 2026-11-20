using System.Linq;
using System.Security.Claims;
using KooliProjekt.Application.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KooliProjekt.WebAPI
{
    public class WebUserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WebUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? UserId()
        {
            var claim = _httpContextAccessor.HttpContext.User.Claims
                .Where(claim => claim.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault();

            if(claim == null)
            {
                return null;
            }

            return int.Parse(claim.Value);
        }
    }
}
