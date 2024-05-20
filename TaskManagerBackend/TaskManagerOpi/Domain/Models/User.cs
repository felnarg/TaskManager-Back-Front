using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class User
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public NotificationPreference? NotificationPreference { get; set; }
        [JsonIgnore]
        public IEnumerable<TaskEntity>? Tasks { get; set; }
    }
}
