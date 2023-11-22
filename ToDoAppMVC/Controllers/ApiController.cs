using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using ToDoAppMVC.Services;

namespace ToDoAppMVC.Controllers;

public class ApiController : Controller
{
    private readonly ITodoService _todoService;
    
    public ApiController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    public async Task<IActionResult> Get([FromQuery]bool? isCompleted)
    {
        var todos = await _todoService.GetTodos();

        if (isCompleted == null)
        {
            return Json(todos);
        }

        var filtered = todos
            .Where(todo => todo.Completed == isCompleted)
            .ToList();
        
        return Json(filtered);
    }
    
    public async Task<IActionResult> GetSingle(int? id)
    {
        var todo = await _todoService.GetTodo(id);
        
        return Json(todo);
    }
    
    
}
