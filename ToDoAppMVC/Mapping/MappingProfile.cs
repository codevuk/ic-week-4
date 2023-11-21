using AutoMapper;
using ToDoAppMVC.Models;
using ToDoAppMVC.ViewModels;

namespace ToDoAppMVC.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoViewModel>();
    }
}