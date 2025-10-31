using System.ComponentModel.DataAnnotations;

namespace Taskboard.API.DTOs.Tasks
{
    public class CreateTaskDTO
    {
        [Required]
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public int Priority { get; set; } = 3;
    }

}
