using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoController(AppDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
    {
        var items = await dbContext.TodoItems
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoItem>> GetById(int id)
    {
        var item = await dbContext.TodoItems.FindAsync(id);
        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create(TodoItem todoItem)
    {
        var item = new TodoItem
        {
            Title = todoItem.Title,
            Description = todoItem.Description,
            IsCompleted = todoItem.IsCompleted,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.TodoItems.Add(item);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TodoItem todoItem)
    {
        var existing = await dbContext.TodoItems.FindAsync(id);
        if (existing is null)
        {
            return NotFound();
        }

        existing.Title = todoItem.Title;
        existing.Description = todoItem.Description;
        existing.IsCompleted = todoItem.IsCompleted;

        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await dbContext.TodoItems.FindAsync(id);
        if (existing is null)
        {
            return NotFound();
        }

        dbContext.TodoItems.Remove(existing);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
