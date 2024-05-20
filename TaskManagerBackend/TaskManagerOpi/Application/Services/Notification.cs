using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    public class Notification : INotification
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Notification (IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task SendNotification(string userEmail, string plainTextContent, string user)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
                {
                var serviceProvider = scope.ServiceProvider;
                var _sendGridService = serviceProvider.GetRequiredService<ISendGridService>();

                await _sendGridService!.SendNotificationEmail(userEmail, plainTextContent, user);
            }                
        }

    }
}
