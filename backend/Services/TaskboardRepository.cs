using MongoDB.Driver;
using Taskboard.API.Models.AppData;
using Microsoft.Extensions.Options;
using Taskboard.API.Config;

namespace Taskboard.Api.Services;

public class TaskboardRepository
{
    private readonly IMongoCollection<BoardDocument> _boards;
    private readonly IMongoCollection<ListColumn> _lists;
    private readonly IMongoCollection<TaskItem> _tasks;

    public TaskboardRepository(IOptions<MongoSettings> mongoSettings)
    {
        var client = new MongoClient(mongoSettings.Value.ConnectionString);
        var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
        _boards = database.GetCollection<BoardDocument>("Boards");
        _lists = database.GetCollection<ListColumn>("Lists");
        _tasks = database.GetCollection<TaskItem>("Tasks");
    }

    // Fetch all boards for a user
    public async Task<List<BoardDocument>> GetBoardsAsync(int userId)
    {
        return await _boards.Find(b => b.UserId == userId).ToListAsync();
    }

    // Get one board
    public async Task<BoardDocument?> GetBoardAsync(string boardId, int userId)
    {
        return await _boards.Find(b => b.Id == boardId && b.UserId == userId).FirstOrDefaultAsync();
    }

    // Create a new board
    public async Task<BoardDocument> CreateBoardAsync(BoardDocument board)
    {
        await _boards.InsertOneAsync(board);
        return board;
    }

    // Update board
    public async Task UpdateBoardAsync(BoardDocument board)
    {
        await _boards.ReplaceOneAsync(b => b.Id == board.Id && b.UserId == board.UserId, board);
    }

    // Delete board
    public async Task DeleteBoardAsync(string boardId, int userId)
    {
        await _boards.DeleteOneAsync(b => b.Id == boardId && b.UserId == userId);
    }

    // Get user's lists for a specific board (top-down: user -> board -> lists)
    public async Task<List<ListColumn>> GetListsAsync(int userId, string boardId)
    {
        // Ensure the board belongs to the user
        var board = await GetBoardAsync(boardId, userId);
        if (board is null) return new List<ListColumn>();

        // Return only lists under that board
        return await _lists.Find(l => l.BoardId == boardId).ToListAsync();
    }

    public async Task<ListColumn> CreateListAsync(ListColumn list)
    {
        await _lists.InsertOneAsync(list);
        return list;
    }

    public async Task UpdateListAsync(ListColumn list)
    {
        await _lists.ReplaceOneAsync(l => l.ListId == list.ListId, list);
    }

    public async Task DeleteListAsync(string listId)
    {
        await _lists.DeleteOneAsync(l => l.ListId == listId);
    }

    // Get list by id only if it belongs to a board owned by the user
    internal async Task<ListColumn?> GetListByIdAsync(string id, int userId)
    {
        var list = await _lists.Find(l => l.ListId == id).FirstOrDefaultAsync();
        if (list is null) return null;

        var board = await GetBoardAsync(list.BoardId, userId);
        return board is null ? null : list;
    }

    // Get tasks for a list under a specific board owned by the user
    internal async Task<List<TaskItem>> GetTasksAsync(int userId, string boardId, string listId)
    {
        var board = await GetBoardAsync(boardId, userId);
        if (board is null) return new();

        var list = await _lists.Find(l => l.ListId == listId && l.BoardId == boardId).FirstOrDefaultAsync();
        if (list is null) return new();

        return await _tasks.Find(t => t.ListId == listId).ToListAsync();
    }

    // Get a single task by id, only if it belongs to the list within the specified board owned by the user
    internal async Task<TaskItem?> GetTaskByIdAsync(string boardId, string listId, string id, int userId)
    {
        var board = await GetBoardAsync(boardId, userId);
        if (board is null) return null;

        var list = await _lists.Find(l => l.ListId == listId && l.BoardId == boardId).FirstOrDefaultAsync();
        if (list is null) return null;

        return await _tasks.Find(t => t.Id == id && t.ListId == listId).FirstOrDefaultAsync();
    }

    // Create a task only if its parent list exists (list -> board enforced indirectly);
    // user/board validation is done by callers that have board/user context.
    internal async Task CreateTaskAsync(TaskItem task)
    {
        var listExists = await _lists.Find(l => l.ListId == task.ListId).AnyAsync();
        if (!listExists)
        {
            throw new KeyNotFoundException("Cannot create task: parent list does not exist.");
        }

        await _tasks.InsertOneAsync(task);
    }

    // Update an existing task (callers should have validated board/list ownership via GetTaskByIdAsync)
    internal async Task UpdateTaskAsync(TaskItem task)
    {
        await _tasks.ReplaceOneAsync(t => t.Id == task.Id && t.ListId == task.ListId, task);
    }

    // Delete an existing task by id (callers validate existence/ownership first)
    internal async Task DeleteTaskAsync(string id)
    {
        await _tasks.DeleteOneAsync(t => t.Id == id);
    }
}
