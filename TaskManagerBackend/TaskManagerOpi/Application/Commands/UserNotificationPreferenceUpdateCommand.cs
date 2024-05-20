using Application._Resources;
using Application.Interfaces;
using Domain.Models;

namespace Application.Commands
{
    public class UserNotificationPreferenceUpdateCommand : IUserNotificationPreferenceUpdateCommand
    {
        private readonly ITaskRepository<User>? _userRepository;

        public UserNotificationPreferenceUpdateCommand(ITaskRepository<User>? userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> NotificationPreferenceUpdate(User user)
        {
            User userDb = await _userRepository!.GetById(Guid.Parse(user.Id!));
            userDb.NotificationPreference = user.NotificationPreference;
            await _userRepository!.Update(userDb);
            return Constants.OK;
        }
    }
}
