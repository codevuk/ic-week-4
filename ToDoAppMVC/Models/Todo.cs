namespace ToDoAppMVC.Models;

public class Todo
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public string Details { get; set; }

    public bool Completed { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public User User { get; set; }
}