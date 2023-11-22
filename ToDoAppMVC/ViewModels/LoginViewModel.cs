using Microsoft.Build.Framework;

namespace ToDoAppMVC.ViewModels;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}