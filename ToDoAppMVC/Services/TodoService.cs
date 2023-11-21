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
}