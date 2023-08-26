using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireApp.Models;
using FireApp.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace FireApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IServiceManagement _serviceManagement;

        public UserController(IUserService userService, IServiceManagement serviceManagement)
        {
            _userService = userService;
            _serviceManagement = serviceManagement;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            await _userService.CreateUserAsync(user);
            // run a fire and forget task to send an email
            var jobId = BackgroundJob.Enqueue(() => _serviceManagement.SendSms());
            // run a fire and forget task to update the database
            var jobId2 = BackgroundJob.Enqueue<IServiceManagement>(x => x.UpdateDatabase()); // this is the same as the line above
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            // RecurringJob.AddOrUpdate<IServiceManagement>(x => x.UpdateDatabase(), Cron.Minutely);
            RecurringJob.AddOrUpdate(() => _serviceManagement.UpdateDatabase(), Cron.Minutely);
            return NoContent();
        }
    }
}