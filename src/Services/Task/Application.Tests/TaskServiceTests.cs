using Api.Configuration;
using Application.Dtos;
using Application.Interfaces.Persistence;
using Application.Services;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using NSubstitute;

namespace Application.Tests;

public class Tests
{
    private IMapper _mapper;
    private IUnitOfWork _unitOfWork;
    private ITaskRepository _taskRepository;
    private TaskService _target;

    [SetUp]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<WebApiMappingProfile>();
        });

        _mapper = new Mapper(config);
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _taskRepository = Substitute.For<ITaskRepository>();

        _target = new TaskService(_mapper, _unitOfWork);
    }

    [Test]
    public void GetAll_ShouldReturnAllTasks()
    {
        // Arrange
        var tasks = new List<TaskEntityDto>
        {
            new() { TaskId = 1, TaskName = "Task 1", TaskDescription = "Description 1", TaskStatus = TaskEntityStatus.ToDo },
            new() { TaskId = 2, TaskName = "Task 2", TaskDescription = "Description 2", TaskStatus = TaskEntityStatus.Completed },
            new() { TaskId = 3, TaskName = "Task 3", TaskDescription = "Description 3", TaskStatus = TaskEntityStatus.Blocked }
        };

        _unitOfWork.Create().Repositories.TaskRepository.GetAll().Returns(tasks);

        // Act
        var result = _target.GetAll();

        // Assert
        Assert.That(result?.Count, Is.EqualTo(tasks.Count));
    }

    [Test]
    public void GetById_ShouldReturnTaskById()
    {
        // Arrange
        var taskId = 1;
        var taskName = "Task 1";
        var taskDescription = "Description 1";
        var taskStatus = TaskEntityStatus.ToDo;
        var task = new TaskEntityDto { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        var expectedTask = new TaskEntityDto { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        _unitOfWork.Create().Repositories.TaskRepository.Get(taskId).Returns(task);

        // Act
        var result = _target.GetById(taskId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.TaskName, Is.EqualTo(expectedTask.TaskName));
            Assert.That(result.TaskDescription, Is.EqualTo(expectedTask.TaskDescription));
            Assert.That(result.TaskStatus, Is.EqualTo(expectedTask.TaskStatus));
        });
    }

    [Test]
    public void Add_ShouldReturnTaskId()
    {
        // Arrange
        var taskId = 1;
        var taskName = "Task 1";
        var taskDescription = "Description 1";
        var taskStatus = TaskEntityStatus.ToDo;
        var newTask = new CreateTaskVm { TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        _unitOfWork.Create().Repositories.TaskRepository.Create(Arg.Any<TaskEntityDto>()).Returns(taskId);

        // Act
        var result = _target.Add(newTask);

        // Assert
        Assert.That(result, Is.EqualTo(taskId));
    }

    [Test]
    public void Update_ShouldUpdatedTask()
    {
        // Arrange
        var taskId = 1;
        var taskName = "Task 1";
        var taskDescription = "Description 1";
        var taskStatus = TaskEntityStatus.ToDo;
        var updatedTask = new UpdateTaskVm { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };

        _unitOfWork.Create().ReturnsForAnyArgs(Substitute.For<IUnitOfWorkAdapter>());
        _unitOfWork.Create().Repositories.TaskRepository.ReturnsForAnyArgs(_taskRepository);
        
        // Act
        _target.Update(updatedTask);

        // Assert
        _taskRepository.Received(1).Update(Arg.Is<TaskEntityDto>(t =>
            t.TaskName == updatedTask.TaskName &&
            t.TaskDescription == updatedTask.TaskDescription &&
            t.TaskStatus == updatedTask.TaskStatus
        ));
    }

    [Test]
    public void Delete_ShouldDeletedTask()
    {
        // Arrange
        var taskId = 1;
        _unitOfWork.Create().ReturnsForAnyArgs(Substitute.For<IUnitOfWorkAdapter>());
        _unitOfWork.Create().Repositories.TaskRepository.ReturnsForAnyArgs(_taskRepository);

        // Act
        _target.Delete(taskId);

        // Assert
        _taskRepository.Received(1).Remove(Arg.Is<int>(taskId));
    }
}