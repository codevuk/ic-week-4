using ToDoAppMVC.ViewModels;

namespace ToDoAppMVC.Services;

public interface ITodoService
{
    Task<List<TodoViewModel>> GetTodos(int userId);

    Task<TodoViewModel?> GetTodo(int id, int userId);
    
    Task<int> CreateTodo(CreateTodoViewModel model, int userId);
    
    Task<TodoViewModel> UpdateTodo(UpdateTodoViewModel model, int userId);
    
    Task DeleteTodo(int id, int userId);
}