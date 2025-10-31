using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Taskboard.API.Models.AppData
{
    public class BoardDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public int UserId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
        public List<TaskItem> Tasks { get; set; } = new();
    }
}
