using Application.Interfaces;
using Application.ViewModels;
using Domain.Enums;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class ExpiredTasksNotificationQuerie : IExpiredTasksNotificationQuerie
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ExpiredTasksNotificationQuerie(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<IEnumerable<TaskEntity>> GetExpiredTasks()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var _taskRepository = serviceProvider.GetRequiredService<ITaskRepository<TaskEntity>>();

                DateTime currentDate = DateTime.Now;
                IEnumerable<TaskEntity> tasks = await _taskRepository!.GetAll();

                IEnumerable<TaskEntity> expiredTask = tasks.Where(task => (task.ExpirationDate - currentDate)!.Value.TotalDays < 1 &&
                    (task.ExpirationDate - currentDate)!.Value.TotalDays > 0 && task.Status == 0);

                return expiredTask;
            }                
        }

        public async Task<IEnumerable<User>> GetUsersForNotifications(IEnumerable<TaskEntity> tasks)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var _userRepository = serviceProvider.GetRequiredService<ITaskRepository<User>>();

                IEnumerable<User> users = await _userRepository!.GetAll();
                IEnumerable<User> filteredUsers = users.Where(user => tasks.Select(task => task.UserId).Contains(user.Id));

                return filteredUsers;
            }               
        }

        public async Task<IEnumerable<TasksUsersJoinModel>> GetExpiredTasksByUserPreference()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var _taskRepository = serviceProvider.GetRequiredService<ITaskRepository<TaskEntity>>();
                var _userRepository = serviceProvider.GetRequiredService<ITaskRepository<User>>();

                IEnumerable<TaskEntity> tasks = await _taskRepository!.GetAll();
                IEnumerable<User> users = await _userRepository!.GetAll();
                DateTime currentDate = DateTime.Now;

                var tasksUsersJoin = (from task in tasks
                                      join user in users on task.UserId equals user.Id
                                      select new TasksUsersJoinModel
                                      {
                                          Id = task!.Id,
                                          TaskTitle = task.Title,
                                          UserName = user.Name,
                                          UserEmail = user.Email,
                                          NotificationPreference = user.NotificationPreference,
                                          ExpirationDate = task.ExpirationDate,
                                          Status = task.Status

                                      }).ToList();

                IEnumerable<TasksUsersJoinModel> expiredTaskOneDay = tasksUsersJoin.Where(task => (task.ExpirationDate - currentDate)!.Value.TotalDays <= 1 &&
                (task.ExpirationDate - currentDate)!.Value.TotalDays > 0 && task.Status == 0 && task.NotificationPreference == NotificationPreference.OneDay);                               
                
                IEnumerable<TasksUsersJoinModel> expiredTaskOneWeek = tasksUsersJoin.Where(task => (task.ExpirationDate - currentDate)!.Value.TotalDays <= 7 &&
                (task.ExpirationDate - currentDate)!.Value.TotalDays > 0 && task.Status == 0 && task.NotificationPreference == NotificationPreference.OneWeek);                
                
                IEnumerable<TasksUsersJoinModel> expiredTaskOneHour = tasksUsersJoin.Where(task => (task.ExpirationDate - currentDate)!.Value.TotalHours <= 1 &&
                (task.ExpirationDate - currentDate)!.Value.TotalHours > 0 && task.Status == 0 && task.NotificationPreference == NotificationPreference.OneHour);

                IEnumerable <TasksUsersJoinModel> totalExpiredTasks = expiredTaskOneHour.Concat(expiredTaskOneDay).Concat(expiredTaskOneWeek);

                return totalExpiredTasks;
            }
        }
    }
}
