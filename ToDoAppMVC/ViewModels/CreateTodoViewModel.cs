using System.ComponentModel.DataAnnotations;

namespace ToDoAppMVC.ViewModels;

public class CreateTodoViewModel
{
    [Required]
    [MinLength(5)]
    public string Details { get; set; }
}