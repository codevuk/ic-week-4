using ToDoAppMVC.ViewModels;

namespace ToDoAppMVC.Services;

public interface ITodoService
{
    Task<List<TodoViewModel>> GetTodos();
}