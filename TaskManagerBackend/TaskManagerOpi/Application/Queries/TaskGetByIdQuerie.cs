using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Oracle.EntityFrameworkCore.Query.Internal;

namespace Application.Queries
{
    public class TaskGetByIdQuerie : ITaskGetById
    {
        private readonly ITaskRepository<TaskEntity>? _taskRepository;

        public TaskGetByIdQuerie(ITaskRepository<TaskEntity> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskEntity> TaskGetById(Guid id)
        {
            return await _taskRepository!.GetById(id);
        }
    }
}
