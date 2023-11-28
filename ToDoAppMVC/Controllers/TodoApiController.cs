using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoAppMVC.Services;
using ToDoAppMVC.ViewModels;

namespace ToDoAppMVC.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TodoApiController : BaseAuthController
{
    private readonly ITodoService _todoService;
    
    public TodoApiController(ITodoService todoService)
    {
        _todoService = todoService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(typeof(IEnumerable<TodoViewModel>))]
    public async Task<IActionResult> Get([FromQuery]bool? isCompleted)
    {
        var todos = await _todoService.GetTodos(GetUserId());

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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(typeof(TodoViewModel))]
    public async Task<IActionResult> GetSingle(int id)
    {
        var todo = await _todoService.GetTodo(id, GetUserId());

        if (todo == null)
        {
            return NotFound("Could not find todo");
        }
        
        return Ok(todo);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateTodo([FromBody]CreateTodoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var id = await _todoService.CreateTodo(model, GetUserId());

        return Ok(id);
    }
}
