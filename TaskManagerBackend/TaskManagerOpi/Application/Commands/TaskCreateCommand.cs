using Application._Resources;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using System;

namespace Application.Commands
{
    public class TaskCreateCommand : ITaskCreate
    {
        private readonly ITaskRepository<TaskEntity>? _taskRepository;
        private readonly ITaskRepository<User>? _userTaskRepository;

        public TaskCreateCommand(ITaskRepository<TaskEntity>? taskRepository, ITaskRepository<User> userTaskRepository)
        {
            _taskRepository = taskRepository;
            _userTaskRepository = userTaskRepository;
        }

        public async Task<string> TaskCreate(TaskEntity entity)
        {            
            entity.Id = Guid.NewGuid().ToString();

            Guid guidUserId = Guid.Parse(entity.UserId!);
            await _taskRepository!.SaveEntityAsync(entity);
            return Constants.OK;
        }
    }
}
