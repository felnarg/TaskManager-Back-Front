using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class OrderTasks : IOrderTasks
    {
        private readonly ITaskRepository<TaskEntity>? _taskRepository;

        public OrderTasks(ITaskRepository<TaskEntity> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskEntity>> OrderTasksByImportance()
        {
            IEnumerable<TaskEntity> tasks = await _taskRepository!.GetAll();
            return tasks.OrderBy(task => task.Importance);
        }
    }
}
