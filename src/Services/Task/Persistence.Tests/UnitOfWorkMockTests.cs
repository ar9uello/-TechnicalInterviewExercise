using Application.Dtos;
using Application.Interfaces.Persistence;
using Domain.Enums;
using Moq;

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
        var expectedTasks = new List<TaskEntityDto> { };
        _mockUnitOfWorkRepository.Setup(unitOfWork => unitOfWork.TaskRepository.GetAll()).Returns(expectedTasks);

        // Act
        var tasks = _unitOfWorkRepository.TaskRepository.GetAll();

        // Assert
        Assert.That(tasks, Is.EqualTo(expectedTasks));
    }

    [Test]
    public void GetById_ShouldReturnTaskById()
    {
        // Arrange
        var taskId = 1;
        var taskName = "Task 1";
        var taskDescription = "Description 1";
        var taskStatus = TaskEntityStatus.ToDo;
        var expectedTask = new TaskEntityDto { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        _mockUnitOfWorkRepository.Setup(unitOfWork => unitOfWork.TaskRepository.GetById(taskId)).Returns(expectedTask);

        // Act
        var result = _unitOfWorkRepository.TaskRepository.GetById(taskId);

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
        var newTask = new TaskEntityDto { TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
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
        var updatedTask = new TaskEntityDto { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        _mockUnitOfWorkRepository.Setup(unitOfWork => unitOfWork.TaskRepository.Update(updatedTask));

        // Act
        _unitOfWorkRepository.TaskRepository.Update(updatedTask);

        // Assert
        _mockUnitOfWorkRepository.Verify(unitOfWork => unitOfWork.TaskRepository.Update(updatedTask), Times.Once);
    }

    [Test]
    public void Delete_ShouldDeleteTask()
    {
        // Arrange
        var taskId = 1;
        _mockUnitOfWorkRepository.Setup(unitOfWork => unitOfWork.TaskRepository.Delete(taskId));

        // taskId
        _unitOfWorkRepository.TaskRepository.Delete(taskId);

        // Assert
        _mockUnitOfWorkRepository.Verify(unitOfWork => unitOfWork.TaskRepository.Delete(taskId), Times.Once);
    }
}
