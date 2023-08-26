using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireApp.Models;

namespace FireApp.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task CreateUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(int userId);
    }
}