using Application.Interfaces;
using Domain.Enums;
using Domain.Models;

namespace Application.Queries
{
    public class ImportanceFilter : IImportanceFilter
    {
        private readonly ITaskRepository<TaskEntity>? _taskRepository;

        public ImportanceFilter(ITaskRepository<TaskEntity> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskEntity>> FilterByImportanceLevel(Importance importance)
        {
            IEnumerable<TaskEntity> taskList = await _taskRepository!.GetAll();

            if (importance == Importance.Low)                            
                return taskList.Where(task => task.Importance == Importance.Low);                           

            else if (importance == Importance.Medium)
                return taskList.Where(task => task.Importance == Importance.Medium);

            else
                return taskList.Where(task => task.Importance == Importance.High);
        }
    }
}
