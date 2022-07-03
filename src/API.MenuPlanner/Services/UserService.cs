using API.MenuPlanner.Dtos;
using API.MenuPlanner.Entities;
using API.MenuPlanner.Repositories;
using API.MenuPlanner.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.MenuPlanner.Services
{
    public class UserService
    {
        IRepository<User> _userRepository;
        IPasswordHasher<User> _passwordHasher;
        ConfigurationModel _configuration;
        HttpContextService _httpContextService;

        public UserService(IRepository<User> userRepository,
            IPasswordHasher<User> passwordHasher,
            ConfigurationModel configuration, 
            HttpContextService httpContextService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _httpContextService = httpContextService;
        }

        public async Task RegisterUserAsync(User user)
        {
            User existingUser = await _userRepository.FirstOrDefaultAsync(u => u.Email == user.Email);
            if(existingUser != null)
            {
                throw new ExceptionResponse.ForbidException($"Email {user.Email} is taken");
            }

            User newUser = new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = RoleEnum.Creator,
            };

            newUser.Password = _passwordHasher.HashPassword(newUser, user.Password);

            await _userRepository.AddAsync(newUser);
        }

        public async Task<LoginDto.Response> Login(LoginDto.Request loginRequest)
        {
            User user = await _userRepository.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
            if (user?.Id == null)
                throw new Exception("Invalid email or password");

            var verifyPasswordResult = _passwordHasher.VerifyHashedPassword(user, user.Password, loginRequest.Password);
            if(verifyPasswordResult == PasswordVerificationResult.Failed)
                throw new Exception("Invalid email or password");

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddSeconds(_configuration.JwtExpireSeconds);

            var token = new JwtSecurityToken(
                _configuration.JwtIssuer,
                _configuration.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            LoginDto.Response loginResponse = new LoginDto.Response()
            {
                Token = tokenHandler.WriteToken(token),
                AuthorizationMethod = "Bearer",
            };
            return loginResponse;
        }

        public async Task<UserDto> Profile()
        {
            string? userid = _httpContextService.UserId;
            if (userid == null)
                throw new Exception("Your token is not correct");

            User user = await _userRepository.FirstOrDefaultAsync(u => u.Id == userid);
            if (user?.Id == null)
                throw new Exception("User not found.");

            return new UserDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Role = user.Role.ToString(),
            };
        }
    }
}
