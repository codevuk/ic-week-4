using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ToDoAppMVC.Models;
using ToDoAppMVC.ViewModels;

namespace ToDoAppMVC.Services;

public class TodoService : ITodoService
{
    private readonly TodoDBContext _context;
    private readonly IMapper _mapper;

    public TodoService(TodoDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<TodoViewModel>> GetTodos()
    {
        var todos = await _context.Todos.ToListAsync();

        return _mapper.Map<List<TodoViewModel>>(todos);
    }
    
    public async Task<TodoViewModel> GetTodo(int? id)
    {
        var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<TodoViewModel>(todo);
    }

    public async Task<int> CreateTodo(CreateTodoViewModel model)
    {
        var todo = new Todo
        {
            Details = model.Details,
            Completed = false,
            CreatedAt = DateTime.Now
        };

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return todo.Id;
    }

    public async Task<TodoViewModel> UpdateTodo(UpdateTodoViewModel model)
    {
        
        var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == model.Id);

        if (todo == null)
        {
            throw new ApplicationException();
        }

        todo.Details = model.Details;
        todo.Completed = model.Completed;

        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();

        return _mapper.Map<TodoViewModel>(todo);
    }

    public async Task DeleteTodo(int id)
    {
        var todo = new Todo { Id = id };

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
    }
}