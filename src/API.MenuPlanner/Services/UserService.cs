using API.MenuPlanner.Entities;
using API.MenuPlanner.Repositories;
using Microsoft.AspNetCore.Identity;

namespace API.MenuPlanner.Services
{
    public class UserService
    {
        IRepository<User> _userRepository;
        IPasswordHasher<User> _passwordHasher;

        public UserService(IRepository<User> userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task RegisterUserAsync(User user)
        {
            User existingUser = await _userRepository.FirstOrDefaultAsync(u => u.Email == user.Email);
            if(existingUser != null)
            {
                throw new Exception($"Email {user.Email} is taken");
            }

            User newUser = new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            newUser.Password = _passwordHasher.HashPassword(newUser, user.Password);

            await _userRepository.AddAsync(newUser);
        }
    }
}
