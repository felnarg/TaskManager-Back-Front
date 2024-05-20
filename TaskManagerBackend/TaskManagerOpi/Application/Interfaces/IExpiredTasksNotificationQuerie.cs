using Application.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IExpiredTasksNotificationQuerie
    {
        Task<IEnumerable<TaskEntity>> GetExpiredTasks();
        Task<IEnumerable<User>> GetUsersForNotifications(IEnumerable<TaskEntity> tasks);
        Task<IEnumerable<TasksUsersJoinModel>> GetExpiredTasksByUserPreference();
    }
}
