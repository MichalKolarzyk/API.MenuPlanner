using System.Security.Claims;

namespace API.MenuPlanner.Services
{
    public class HttpContextService
    {
        IHttpContextAccessor _httpContextAccessor;

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public string? UserId 
        { 
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                return user?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            } 
        }
    }
}
