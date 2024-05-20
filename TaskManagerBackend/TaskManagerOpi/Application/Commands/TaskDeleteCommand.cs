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
    public class TaskDeleteCommand : ITaskDelete
    {
        private readonly ITaskRepository<TaskEntity>? _taskRepository;

        public TaskDeleteCommand(ITaskRepository<TaskEntity>? taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<string> TaskDelete(Guid id)
        {
            await _taskRepository!.Delete(id);
            return Constants.OK;
        }
    }
}
