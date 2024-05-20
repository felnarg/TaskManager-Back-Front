using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerOpi.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserNotificationPreferenceUpdateCommand? _notificationPreferenceUpdateCommand;
        private readonly IUserUpdate? _userUpdate;

        public UserController(IUserNotificationPreferenceUpdateCommand notificationPreferenceUpdateCommand, IUserUpdate userUpdate) 
        {
            _userUpdate = userUpdate;
            _notificationPreferenceUpdateCommand = notificationPreferenceUpdateCommand;
        }

        [HttpPut]
        [Route("notificationpreference")]
        public async Task<IActionResult> UserNotificationPreferenceSelected([FromBody] User user)
        {
            await _notificationPreferenceUpdateCommand!.NotificationPreferenceUpdate(user);
            return Ok();
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UserUpdate([FromBody] User entity)
        {
            await _userUpdate!.UpdateUserAsync(entity);
            return Ok();
        }
    }
}
