using System.ComponentModel.DataAnnotations;

namespace Taskboard.API.DTOs.Tasks
{
    public class MoveTaskDTO
    {
        [Required]
        public string FromListId { get; set; } = default!;
        [Required]
        public string ToListId { get; set; } = default!;
        [Required]
        public string TaskId { get; set; } = default!;
    }

}
