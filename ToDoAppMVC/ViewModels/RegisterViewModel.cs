using Microsoft.Build.Framework;

namespace ToDoAppMVC.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    public string Name { get; set; }
}