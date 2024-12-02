
using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Domain;

namespace TodoApi.Models.Data;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TodoItem>().HasData(
            new TodoItem { Id = 1, Name = "Sample Task", IsComplete = false }
        );
    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;
}
