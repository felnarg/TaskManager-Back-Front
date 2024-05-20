using Domain.Models;

namespace Application.Interfaces
{
    public interface IUserNotificationPreferenceUpdateCommand
    {
        Task<string> NotificationPreferenceUpdate(User user);
    }
}
