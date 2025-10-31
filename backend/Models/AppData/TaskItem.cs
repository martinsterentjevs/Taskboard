using Taskboard.API.Models.Auth;
using Taskboard.API.Models.enums;

namespace Taskboard.API.Models.AppData
{
    public class TaskItem
    {
        public string Id { get; set; } =Guid.NewGuid().ToString();
        public string ListId { get; set; } = "";
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public TaskState Status { get; set; } = TaskState.ToDo;
        public int Priority { get; set; } = 3; //1-5 scale
        public int AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }
    }
}