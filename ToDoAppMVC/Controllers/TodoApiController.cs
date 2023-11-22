using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoAppMVC.Services;
using ToDoAppMVC.ViewModels;

namespace ToDoAppMVC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoApiController : ControllerBase
{
    private readonly ITodoService _todoService;
    
    public TodoApiController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    // HTTP POST => localhost:5050/api/todoapi
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get([FromQuery]bool? isCompleted)
    {
        var todos = await _todoService.GetTodos();

        if (isCompleted == null)
        {
            return Ok(todos);
        }

        var filtered = todos
            .Where(todo => todo.Completed == isCompleted)
            .ToList();
        
        return Ok(filtered);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces(typeof(TodoViewModel))]
    public async Task<IActionResult> GetSingle(int? id)
    {
        var todo = await _todoService.GetTodo(id);

        if (todo == null)
        {
            return NotFound("Could not find todo");
        }
        
        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo([FromBody]CreateTodoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var id = await _todoService.CreateTodo(model);

        return Ok(id);
    }
}
