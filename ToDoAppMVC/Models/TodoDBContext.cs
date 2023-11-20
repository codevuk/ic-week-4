using Microsoft.EntityFrameworkCore;

namespace ToDoAppMVC.Models;

public partial class TodoDBContext : DbContext
{
    public TodoDBContext(DbContextOptions<TodoDBContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}