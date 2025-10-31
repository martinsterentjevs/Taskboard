namespace Taskboard.API.Models.AppData
{
    public class ListColumn
    {
        public string BoardId { get; set; } = "";
        public string ListId { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public int Order { get; set; }
        public List<TaskItem> Tasks { get; set; } = new();
    }
}
