using Application._Resources;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class ChangeStatusTaskCommand : IChangeStatusTask
    {
        private readonly ITaskRepository<TaskEntity>? _taskRepository;

        public ChangeStatusTaskCommand(ITaskRepository<TaskEntity>? taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<string> ChangeStatusTask(Guid id)
        {
            TaskEntity taskEntity = await _taskRepository!.GetById(id);

            taskEntity.Status = (taskEntity.Status == Status.Active) ? Status.Disabled : Status.Active;
            await _taskRepository.Update(taskEntity);
            return Constants.OK;
        }
    }
}
