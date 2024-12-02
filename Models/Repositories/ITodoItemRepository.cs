using TodoApi.Models.Domain;
using TodoApi.Models.DTOs;

namespace TodoApi.Models.Repositories
{
    public interface ITodoItemRepository
    {
        Task<IEnumerable<TodoItem>> GetAllTodosAsync(int pageNumber, int pageSize);
        Task<TodoItem?> GetTodo(long id);
        Task AddTodoAsync(TodoItem todoItem);
        Task UpdateTodoAsync(TodoItem todoItem);
        Task DeleteTodoAsync(TodoItem todoItem);
        Task<bool> TodoItemExistsAsync(long id);
    }
}
