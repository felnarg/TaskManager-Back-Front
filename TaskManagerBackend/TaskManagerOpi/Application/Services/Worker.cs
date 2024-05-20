using Application._Resources;
using Application.Interfaces;
using Application.Queries;
using Application.ViewModels;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace Application.Services
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopedFactory;
        private readonly INotification _notification;
        private readonly IExpiredTasksNotificationQuerie _expiredTasksNotificationQuerie;
        private IEnumerable<TasksUsersJoinModel> _CompareList;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory, INotification notification, IExpiredTasksNotificationQuerie expiredTasksNotificationQuerie)
        {
            _scopedFactory = serviceScopeFactory;
            _logger = logger;
            _notification = notification;
            _expiredTasksNotificationQuerie = expiredTasksNotificationQuerie;
            _CompareList = new List<TasksUsersJoinModel>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(Resource1.LoggerSendGridMessage, DateTimeOffset.Now);

                IEnumerable<TasksUsersJoinModel> tasksUsersJoin = await _expiredTasksNotificationQuerie.GetExpiredTasksByUserPreference();
                IEnumerable<TasksUsersJoinModel> result = tasksUsersJoin.Where(task => !_CompareList.Any(c => c.Id == task.Id && c.Id == task.Id)).ToList();

                await SendEmails(result);

                Console.WriteLine(result.Count());
                _CompareList = _CompareList.Concat(result).DistinctBy(task => task.Id);

                Task.Delay(TimeSpan.FromMinutes(0.1)).Wait();

            }
        }

        private async Task SendEmails(IEnumerable<TasksUsersJoinModel> tasksUsersJoin)
        {
            if (tasksUsersJoin.Any())
            {
                foreach (var task in tasksUsersJoin)
                {
                    string message = string.Format(Resource1.EmailBodyMessage, task.TaskTitle);
                    await _notification!.SendNotification(task.UserEmail!, message, task.UserName!);
                }
            }
        }

        
    }
}
