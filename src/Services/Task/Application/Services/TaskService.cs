using Application.Dtos;
using Application.Interfaces;
using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class TaskService : ITaskService
{
    private readonly IMapper _mapper;
    private IUnitOfWork _unitOfWork;

    public TaskService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public List<TaskEntityDto> GetAll()
    {
        using var context = _unitOfWork.Create();
        var tasks = context.Repositories.TaskRepository.GetAll();
        return _mapper.Map<List<TaskEntityDto>>(tasks);
    }

    public TaskEntityDto GetById(int id)
    {
        using var context = _unitOfWork.Create();
        var tasks = context.Repositories.TaskRepository.Get(id);
        return _mapper.Map<TaskEntityDto>(tasks);
    }

    public int Add(TaskEntityDto taskDto)
    {
        using var context = _unitOfWork.Create();
        var task = _mapper.Map<TaskEntity>(taskDto);
        return context.Repositories.TaskRepository.Create(task);
    }

    public void Update(TaskEntityDto taskDto)
    {
        using var context = _unitOfWork.Create();
        var task = _mapper.Map<TaskEntity>(taskDto);
        context.Repositories.TaskRepository.Update(task);
    }

    public void Delete(int id)
    {
        using var context = _unitOfWork.Create();
        context.Repositories.TaskRepository.Remove(id);
    }

}
