using Application.Dtos;
using Application.ViewModels;
using AutoMapper;

namespace Api.Configuration;

public class WebApiMappingProfile : Profile
{
    public WebApiMappingProfile()
    {
        CreateMap<CreateTaskVm, TaskEntityDto>();
        CreateMap<UpdateTaskVm, TaskEntityDto>();
        CreateMap<TaskEntityDto, GetTaskVm>();
    }
}
