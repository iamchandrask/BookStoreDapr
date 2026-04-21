using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingBlocks.Contracts;
using UserService.Domain;
using UserService.Infrastructure;

namespace UserService.Application
{
    public interface IUserServiceApp
    {
        Task<UserResponse> CreateUserAsync(CreateUserRequest request);
        Task<UserResponse?> GetUserByIdAsync(Guid id);
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
    }

    public class UserServiceApp : IUserServiceApp
    {
        private readonly IUserRepository _userRepository;

        public UserServiceApp(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> CreateUserAsync(CreateUserRequest request)
        {
            var user = new User
            {
                Email = request.Email,
                FullName = request.FullName
            };

            var created = await _userRepository.CreateAsync(user);

            return new UserResponse
            {
                Id = created.Id,
                Email = created.Email,
                FullName = created.FullName
            };
        }

        public async Task<UserResponse?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserResponse
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName
            });
        }
    }
}