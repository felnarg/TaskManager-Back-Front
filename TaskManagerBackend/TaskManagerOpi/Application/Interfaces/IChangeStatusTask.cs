using Domain.Enums;

namespace Application.Interfaces
{
    public interface IChangeStatusTask
    {
        Task<string> ChangeStatusTask(Guid id);
    }
}
