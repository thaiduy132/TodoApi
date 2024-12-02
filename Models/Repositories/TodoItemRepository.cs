using Microsoft.EntityFrameworkCore;
using System.Linq;
using TodoApi.Models.Data;
using TodoApi.Models.Domain;
using TodoApi.Models.DTOs;

namespace TodoApi.Models.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext _context;

        public TodoItemRepository(TodoContext context)
        {
            _context = context;
        }

        async Task ITodoItemRepository.AddTodoAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
        }


        async Task ITodoItemRepository.DeleteTodoAsync(TodoItem todoItem)
        {

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            
        }

        async Task<IEnumerable<TodoItem>> ITodoItemRepository.GetAllTodosAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }


        async Task<TodoItem?> ITodoItemRepository.GetTodo(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        async Task ITodoItemRepository.UpdateTodoAsync(TodoItem todoItem)
        {
            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> TodoItemExistsAsync(long id)
        {
            return await _context.TodoItems.AnyAsync(e => e.Id == id);
        }


    }
}
