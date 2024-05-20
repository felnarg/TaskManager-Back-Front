using Application._Resources;
using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class TaskUpdateCommand : ITaskUpdate
    {
        private readonly ITaskRepository<TaskEntity>? _taskRepository;

        public TaskUpdateCommand(ITaskRepository<TaskEntity>? taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<string> TaskUpdate(TaskEntity entity)
        {
            await _taskRepository!.Update(entity);
            return Constants.OK;
        }
    }
}
