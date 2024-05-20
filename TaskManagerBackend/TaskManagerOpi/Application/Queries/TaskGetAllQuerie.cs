using Application.Interfaces;
using Application.ViewModels;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class TaskGetAllQuerie : ITaskGetAll
    {
        private readonly ITaskRepository<TaskEntity>? _taskRepository;

        public TaskGetAllQuerie(ITaskRepository<TaskEntity> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskEntityModel>> TaskGetAll()
        {
            IEnumerable<TaskEntity> tasks =  await _taskRepository!.GetAll();
            IEnumerable<TaskEntityModel> tasksModel = tasks.Select(task => new TaskEntityModel
            {
                Id = task.Id,
                UserId = task.UserId,
                Title = task.Title,
                Status = task.Status,
                Importance = task.Importance,
                ExpirationDate = task.ExpirationDate,
            }).OrderByDescending(task => task.Importance);

            return tasksModel;
        }
    }
}
