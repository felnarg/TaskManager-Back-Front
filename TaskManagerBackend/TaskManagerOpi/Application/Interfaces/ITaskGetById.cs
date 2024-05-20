using Domain.Models;
using static Application.Queries.TaskGetByIdQuerie;

namespace Application.Interfaces
{
    public interface ITaskGetById
    {
        Task<TaskEntity> TaskGetById(Guid id);
    }
}
