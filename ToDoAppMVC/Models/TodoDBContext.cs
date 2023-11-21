using Microsoft.EntityFrameworkCore;
using ToDoAppMVC.ViewModels;

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
        modelBuilder.Entity<Todo>()
            .Property(t => t.CreatedAt)
            .HasColumnType("datetime");
        
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    public DbSet<ToDoAppMVC.ViewModels.TodoViewModel> TodoViewModel { get; set; } = default!;
}