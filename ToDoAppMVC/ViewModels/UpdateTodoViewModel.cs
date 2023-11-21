namespace ToDoAppMVC.ViewModels;

public class UpdateTodoViewModel
{
    public int Id { get; set; }
    
    public string Details { get; set; }

    public bool Completed { get; set; }
}