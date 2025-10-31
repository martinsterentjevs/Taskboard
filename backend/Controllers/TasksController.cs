using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Taskboard.Api.Services;
using Taskboard.API.DTOs.Tasks;

namespace Taskboard.API.Controllers
{
    [Route("api/boards/{boardId}/lists/{listId}/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private TaskboardRepository _repo;
        public TasksController(TaskboardRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks(string boardId, string listId)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            var tasks = await _repo.GetTasksAsync(userId, boardId, listId);
            return Ok(tasks);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask(string boardId, string listId, CreateTaskDTO dto)
        {
            var tasks = new Models.AppData.TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                Status = Models.enums.TaskState.ToDo,
                ListId = listId,
                CreatedAt = DateTime.UtcNow
            };
            await _repo.CreateTaskAsync(tasks);
            return Ok(tasks);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(string boardId, string listId, string id, UpdateTaskDTO dto)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            var existingTask = await _repo.GetTaskByIdAsync(boardId, listId, id, userId);
            if (existingTask is null) return NotFound();
            existingTask.Title = dto.Title ?? existingTask.Title;
            existingTask.Description = dto.Description ?? existingTask.Description;
            existingTask.Priority = dto.Priority != 0 ? dto.Priority : existingTask.Priority;
            existingTask.Status = dto.Status;
            await _repo.UpdateTaskAsync(existingTask);
            return Ok(existingTask);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(string boardId, string listId,string id)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            if (await _repo.GetTaskByIdAsync(boardId, listId, id, userId) is null) // Check if the task exists
            {
                return NotFound();
            }
            await _repo.DeleteTaskAsync(id);
            return Ok($"Task {id} deleted successfully");
        }
    }
}
