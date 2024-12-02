using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Data;
using TodoApi.Models.Domain;
using TodoApi.Models.DTOs;
using TodoApi.Models.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemsController(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var todoItems = await _todoItemRepository.GetAllTodosAsync();

            return todoItems.Select(x => ItemToDTO(x)).ToList();

        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoItemRepository.GetTodo(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoDTO)
        {
           if ( id != todoDTO.Id) return BadRequest();

           var todoItem = await _todoItemRepository.GetTodo(id);

            if (todoItem == null)
            {
                return NotFound();
            }
            todoItem.Name = todoDTO.Name;
            todoItem.IsComplete = todoDTO.IsComplete;

            try
            {
                await _todoItemRepository.UpdateTodoAsync(todoItem);

            }catch (DbUpdateConcurrencyException)
            {
                bool exists = !await _todoItemRepository.TodoItemExistsAsync(id);
                if (!exists) return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoDTO)
        {
            var todoItem = new TodoItem
            {
                Name = todoDTO.Name,
                IsComplete = todoDTO.IsComplete,
            };

            await _todoItemRepository.AddTodoAsync(todoItem);

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, ItemToDTO(todoItem));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {

            var todoItem = await _todoItemRepository.GetTodo(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            await _todoItemRepository.DeleteTodoAsync(todoItem);

            return NoContent();

        }

       

        private static TodoItemDTO ItemToDTO(TodoItem item) =>
            new TodoItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete,
            };
    }
}
