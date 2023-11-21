using Microsoft.AspNetCore.Mvc;
using ToDoAppMVC.Services;
using ToDoAppMVC.ViewModels;

namespace ToDoAppMVC.Controllers;

public class TodoController : Controller
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }
    public async Task<IActionResult> Index()
    {
        var todos = await _todoService.GetTodos();
        
        return View(todos);
    }
    
    public async Task<IActionResult> Create()
    {
        return View();
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var todo = await _todoService.GetTodo(id);
    
        return View(todo);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Details")]CreateTodoViewModel model)
    {
        // We should do some error here
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }
        
        var id = await _todoService.CreateTodo(model);

        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var todo = await _todoService.GetTodo(id);
    
        return View(todo);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Bind("Id,Details,Completed")]UpdateTodoViewModel model)
    {
        // We should do some error here
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }
        
        var id = await _todoService.UpdateTodo(model);

        return RedirectToAction(nameof(Index));
    }
}