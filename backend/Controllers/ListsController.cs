using Microsoft.AspNetCore.Mvc;
using Taskboard.Api.Services;
using Taskboard.API.DTOs.Lists;

namespace Taskboard.API.Controllers
{
    [Route("api/boards/{boardId}/[controller]")]

    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly TaskboardRepository _repo;
        public ListsController(TaskboardRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetLists(string boardId)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            var lists = await _repo.GetListsAsync(userId, boardId);
            return Ok(lists);
        }

        [HttpPost]
        public async Task<IActionResult> CreateList(string boardId,[FromBody]CreateListDTO dto)
        {
            var list = new Models.AppData.ListColumn
            {
                Title = dto.Title,
                BoardId = boardId,
                CreatedAt = DateTime.UtcNow
            };
            await _repo.CreateListAsync(list);
            return Ok($"List {dto.Title} created successfully");
        }
        [HttpPut ("{id}")]
        public async Task<IActionResult> UpdateList(string id, [FromBody]UpdateListDTO dto)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            var existingList = await _repo.GetListByIdAsync(id, userId);
            if (existingList is null) return NotFound();
            existingList.Title = dto.Title ?? existingList.Title;
            await _repo.UpdateListAsync(existingList);
            return Ok(existingList);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(string id)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            if (await _repo.GetListByIdAsync(id, userId) is null) // Check if the list exists
            {
                return NotFound();
            }
            await _repo.DeleteListAsync(id);
            return NoContent();
        }
    }
}
