using API.MenuPlanner.Entities;

namespace API.MenuPlanner.Dtos
{
    public class LoginDto
    {
        public class Response
        {
            public string Token { get; set; } = string.Empty;
            public string AuthorizationMethod { get; set; } = string.Empty;
        }

        public class Request
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }


}
