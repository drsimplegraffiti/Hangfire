using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireApp.Data;
using FireApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FireApp.Services
{
    public class UserService : IUserService
    {

         private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
         public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public Task UpdateUserAsync(User user)
        {
            var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            userToUpdate!.Name = user.Name;
            userToUpdate.Email = user.Email;
            return _context.SaveChangesAsync();
        }

        public Task DeleteUserAsync(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            _context.Users.Remove(user!);
            return _context.SaveChangesAsync();
        }
    }
}