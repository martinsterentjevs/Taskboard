using Taskboard.API.Models.enums;

namespace Taskboard.API.DTOs.Tasks
{
    public class UpdateTaskDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? Completed { get; set; }
        public int Priority { get; set; }
        public TaskState Status { get; set; }
    }

}
