using Application.Interfaces.Persistence;
using Domain.Entities;
using Domain.Enums;
using Moq;
using Persistence.Repositories;

namespace Persistence.Tests;

public class UnitOfWorkMockTests
{
    private IUnitOfWorkRepository _unitOfWorkRepository;
    private Mock<IUnitOfWorkRepository> _mockUnitOfWorkRepository;

    [SetUp]
    public void Setup()
    {
        _mockUnitOfWorkRepository = new Mock<IUnitOfWorkRepository>();
        _unitOfWorkRepository = _mockUnitOfWorkRepository.Object;
    }

    [Test]
    public void GetAll_ShouldReturnAllTasks()
    {
        // Arrange
        var expectedTasks = new List<TaskEntity> { };
        _mockUnitOfWorkRepository.Setup(unitOfWork => unitOfWork.TaskRepository.GetAll()).Returns(expectedTasks);

        // Act
        var tasks = _unitOfWorkRepository.TaskRepository.GetAll();

        // Assert
        Assert.That(tasks, Is.EqualTo(expectedTasks));
    }

    [Test]
    public void Get_ShouldReturnTaskById()
    {
        // Arrange
        var taskId = 1;
        var taskName = "Task 1";
        var taskDescription = "Description 1";
        var taskStatus = TaskEntityStatus.ToDo;
        var expectedTask = new TaskEntity { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        _mockUnitOfWorkRepository.Setup(unitOfWork => unitOfWork.TaskRepository.Get(taskId)).Returns(expectedTask);

        // Act
        var result = _unitOfWorkRepository.TaskRepository.Get(taskId);

        // Assert
        Assert.That(result, Is.EqualTo(expectedTask));
    }

    [Test]
    public void Create_ShouldReturnTaskId()
    {
        // Arrange
        var taskName = "Task 1";
        var taskDescription = "Description 1";
        var taskStatus = TaskEntityStatus.ToDo;
        var newTask = new TaskEntity { TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        var expectedTaskId = 1;
        _mockUnitOfWorkRepository.Setup(unitOfWork => unitOfWork.TaskRepository.Create(newTask)).Returns(expectedTaskId);

        // Act
        var taskId = _unitOfWorkRepository.TaskRepository.Create(newTask);

        // Assert
        Assert.That(taskId, Is.Not.EqualTo(0));
    }

    [Test]
    public void Update_ShouldUpdatedTask()
    {
        // Arrange
        var taskId = 1;
        var taskName = "Task 1";
        var taskDescription = "Description 1";
        var taskStatus = TaskEntityStatus.ToDo;
        var updatedTask = new TaskEntity { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        _mockUnitOfWorkRepository.Setup(unitOfWork => unitOfWork.TaskRepository.Update(updatedTask));

        // Act
        _unitOfWorkRepository.TaskRepository.Update(updatedTask);

        // Assert
        _mockUnitOfWorkRepository.Verify(unitOfWork => unitOfWork.TaskRepository.Update(updatedTask), Times.Once);
    }

    [Test]
    public void Remove_ShouldDeleteTask()
    {
        // Arrange
        var taskId = 1;
        _mockUnitOfWorkRepository.Setup(unitOfWork => unitOfWork.TaskRepository.Remove(taskId));

        // taskId
        _unitOfWorkRepository.TaskRepository.Remove(taskId);

        // Assert
        _mockUnitOfWorkRepository.Verify(unitOfWork => unitOfWork.TaskRepository.Remove(taskId), Times.Once);
    }
}
