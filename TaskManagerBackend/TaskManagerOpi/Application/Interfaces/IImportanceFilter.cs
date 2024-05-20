using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IImportanceFilter
    {
        Task<IEnumerable<TaskEntity>> FilterByImportanceLevel(Importance importance);
    }
}
