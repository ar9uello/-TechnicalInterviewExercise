using Application.Dtos;
using Application.Interfaces;
using Application.Interfaces.Persistence;
using Application.ViewModels;
using AutoMapper;

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

    public List<GetTaskVm> GetAll()
    {
        using var context = _unitOfWork.Create();
        var tasks = context.Repositories.TaskRepository.GetAll();
        return _mapper.Map<List<GetTaskVm>>(tasks);
    }

    public GetTaskVm GetById(int id)
    {
        using var context = _unitOfWork.Create();
        var task = context.Repositories.TaskRepository.Get(id);
        return _mapper.Map<GetTaskVm>(task);
    }

    public int Add(CreateTaskVm vm)
    {
        using var context = _unitOfWork.Create();
        var taskDto = _mapper.Map<TaskEntityDto>(vm);
        var taskId = context.Repositories.TaskRepository.Create(taskDto);
        context.SaveChanges();
        return taskId;
    }

    public void Update(UpdateTaskVm vm)
    {
        using var context = _unitOfWork.Create();
        
        var task = context.Repositories.TaskRepository.Get(vm.TaskId);
        vm.TaskName ??= task.TaskName;
        vm.TaskDescription ??= task.TaskDescription;
        vm.TaskStatus ??= task.TaskStatus;

        var taskDto = _mapper.Map<TaskEntityDto>(vm);
        context.Repositories.TaskRepository.Update(taskDto);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        using var context = _unitOfWork.Create();
        context.Repositories.TaskRepository.Remove(id);
        context.SaveChanges();
    }

}
