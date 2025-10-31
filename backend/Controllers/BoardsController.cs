using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskboard.Api.Services;
using Taskboard.API.DTOs.Boards;
using Taskboard.API.Models.AppData;
using Taskboard.API.Models.Auth;

namespace Taskboard.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly TaskboardRepository _repo;

        public BoardsController(TaskboardRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetBoards()
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            var boards = await _repo.GetBoardsAsync(userId);
            return Ok(boards);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoard([FromBody] CreateBoardDTO dto)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            var board = new BoardDocument
            {
                Name = dto.Name,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.CreateBoardAsync(board);
            return Ok(board);
        }
        [HttpPut("{boardId}")]
        public async Task<IActionResult> UpdateBoard(string boardId, [FromBody] UpdateBoardDto dto)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            var existing = await _repo.GetBoardAsync(boardId, userId);
            if (existing is null) return NotFound();

            existing.Name = dto.Name ?? existing.Name;
            existing.Description = dto.Description ?? existing.Description;

            await _repo.UpdateBoardAsync(existing);
            return Ok(existing);
        }
        [HttpDelete("{boardId}")]
        public async Task<IActionResult> DeleteBoard(string boardId)
        {
            var user = int.Parse(User.FindFirst("id")!.Value);
            return await _repo.DeleteBoardAsync(boardId, user);
        }
    }
}