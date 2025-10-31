using System.ComponentModel.DataAnnotations;

namespace Taskboard.API.DTOs.Lists
{
    public class CreateListDTO
    {
        [Required]
        public string Title { get; set; } = default!;
    }
}
