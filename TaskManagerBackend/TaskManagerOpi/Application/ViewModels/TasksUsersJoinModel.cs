using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class TasksUsersJoinModel
    {
        public string? Id { get; set; }
        public string? TaskTitle { get; set; }
        public string? UserEmail { get; set; }
        public string? UserName { get; set; }
        public NotificationPreference? NotificationPreference { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public Status? Status { get; set; }
    }
}
