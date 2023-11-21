using Microsoft.Build.Framework;

namespace ToDoAppMVC.ViewModels;

public class CreateTodoViewModel
{
    [Required]
    public string Details { get; set; }
}