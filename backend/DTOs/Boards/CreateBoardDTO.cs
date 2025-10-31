namespace Taskboard.API.DTOs.Boards
{
    public class CreateBoardDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

    }
}
