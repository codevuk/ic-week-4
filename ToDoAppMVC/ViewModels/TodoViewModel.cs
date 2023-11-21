namespace ToDoAppMVC.ViewModels;

public class TodoViewModel
{
    public int Id { get; set; }

    public string Details { get; set; }

    public bool Completed { get; set; }

    public DateTime CreatedAt { get; set; }
}