using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Api.Configuration;

public class WebApiMappingProfile : Profile
{
    public WebApiMappingProfile()
    {
        CreateMap<TaskEntity, TaskEntityDto>();
        CreateMap<TaskEntityDto, TaskEntity>();
    }
}
