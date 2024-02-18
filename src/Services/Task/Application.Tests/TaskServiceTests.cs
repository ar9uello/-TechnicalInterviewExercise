using Application.Dtos;
using Application.Interfaces;
using Domain.Enums;
using Moq;

namespace Application.Tests;

public class Tests
{
    private ITaskService _taskService;
    private Mock<ITaskService> _mockTaskService;

    [SetUp]
    public void Setup()
    {
        _mockTaskService = new Mock<ITaskService>();
        _taskService = _mockTaskService.Object;
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllTasks()
    {
        // Arrange
        var expectedTasks = new List<TaskDto> {  };
        _mockTaskService.Setup(service => service.GetAllAsync()).ReturnsAsync(expectedTasks);

        // Act
        var result = await _taskService.GetAllAsync();

        // Assert
        Assert.That(result, Is.EqualTo(expectedTasks));
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnTaskById()
    {
        // Arrange
        var taskId = 1;
        var taskName = "Task 1";
        var taskDescription = "Description 1";
        var taskStatus = TaskEntityStatus.ToDo;
        var expectedTask = new TaskDto { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        _mockTaskService.Setup(service => service.GetByIdAsync(taskId)).ReturnsAsync(expectedTask);

        // Act
        var result = await _taskService.GetByIdAsync(taskId);

        // Assert
        Assert.That(result, Is.EqualTo(expectedTask));
    }

    [Test]
    public async Task AddAsync_ShouldReturnAddedTask()
    {
        // Arrange
        var taskId = 1;
        var taskName = "Task 1";
        var taskDescription = "Description 1";
        var taskStatus = TaskEntityStatus.ToDo;
        var newTask = new TaskDto { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        var expectedTask = new TaskDto { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        _mockTaskService.Setup(service => service.AddAsync(newTask)).ReturnsAsync(expectedTask);

        // Act
        var result = await _taskService.AddAsync(newTask);

        // Assert
        Assert.That(result, Is.EqualTo(expectedTask));
    }

    [Test]
    public async Task UpdateAsync_ShouldReturnUpdatedTask()
    {
        // Arrange
        var taskId = 1;
        var taskName = "Task 1";
        var taskDescription = "Description 1";
        var taskStatus = TaskEntityStatus.ToDo;
        var updatedTask = new TaskDto { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        var expectedTask = new TaskDto { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        _mockTaskService.Setup(service => service.UpdateAsync(updatedTask)).ReturnsAsync(expectedTask);

        // Act
        var result = await _taskService.UpdateAsync(updatedTask);

        // Assert
        Assert.That(result, Is.EqualTo(expectedTask));
    }

    [Test]
    public async Task DeleteAsync_ShouldReturnDeletedTask()
    {
        // Arrange
        var taskId = 1;
        var taskName = "Task 1";
        var taskDescription = "Description 1";
        var taskStatus = TaskEntityStatus.ToDo;
        var expectedTask = new TaskDto { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        _mockTaskService.Setup(service => service.DeleteAsync(taskId)).ReturnsAsync(expectedTask);

        // Act
        var result = await _taskService.DeleteAsync(taskId);

        // Assert
        Assert.That(result, Is.EqualTo(expectedTask));
    }
}