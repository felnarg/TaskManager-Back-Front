using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class TaskEntityModel
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public Status? Status { get; set; }
        public Importance? Importance { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? Details { get; set; }
    }
}
