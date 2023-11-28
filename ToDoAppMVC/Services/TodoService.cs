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
    
    public async Task<List<TodoViewModel>> GetTodos(int userId)
    {
        var todos = await _context.Todos
            .Where(todo => todo.UserId == userId)
            .ToListAsync();

        return _mapper.Map<List<TodoViewModel>>(todos);
    }

    public async Task<TodoViewModel?> GetTodo(int id, int userId)
    {
        var todo = await _context.Todos
            .FirstOrDefaultAsync(todo => todo.Id == id && todo.UserId == userId);

        return todo == null ? null : _mapper.Map<TodoViewModel>(todo);
    }

    public async Task<int> CreateTodo(CreateTodoViewModel model, int userId)
    {
        var todo = new Todo
        {
            Details = model.Details,
            Completed = false,
            CreatedAt = DateTime.Now,
            UserId = userId
        };

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return todo.Id;
    }

    public async Task<TodoViewModel> UpdateTodo(UpdateTodoViewModel model, int userId)
    {
        
        var todo = await _context.Todos
            .FirstOrDefaultAsync(t => t.Id == model.Id && t.UserId == userId);

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

    public async Task DeleteTodo(int id, int userId)
    {
        var found = await GetTodo(id, userId);

        if (found == null)
        {
            return;
        }
        
        var todo = new Todo { Id = id };

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
    }
}