using ToDoAppMVC.ViewModels;

namespace ToDoAppMVC.Services;

public interface ITodoService
{
    Task<List<TodoViewModel>> GetTodos();

    Task<TodoViewModel> GetTodo(int? id);
    
    Task<int> CreateTodo(CreateTodoViewModel model);
    
    Task<TodoViewModel> UpdateTodo(UpdateTodoViewModel model);
}