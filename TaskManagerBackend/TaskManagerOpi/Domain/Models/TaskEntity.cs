using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class TaskEntity
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public Status? Status { get; set; }
        public Importance? Importance { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? Details { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }
}
