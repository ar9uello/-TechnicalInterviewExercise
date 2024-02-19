using Application.Dtos;
using Domain.Entities;
using Domain.Enums;
using Persistence.Repositories;
using System.Data.SqlClient;

namespace Persistence.Tests;

[TestFixture]
public class TaskRepositoryTests
{
    private SqlConnection _connection;
    private SqlTransaction _transaction;
    private TaskRepository _taskRepository;

    [SetUp]
    public void SetUp()
    {
        string connectionString = "workstation id=rarguelloDB.mssql.somee.com;packet size=4096;user id=rarguello_SQLLogin_1;pwd=7crtavthl7;data source=rarguelloDB.mssql.somee.com;persist security info=False;Encrypt=False;initial catalog=rarguelloDB";
        _connection = new SqlConnection(connectionString);
        _connection.Open();
        _transaction = _connection.BeginTransaction();

        _taskRepository = new TaskRepository(_connection, _transaction);
    }

    [TearDown]
    public void TearDown()
    {
        _transaction.Rollback(); 
        _connection.Close();
    }

    [Test]
    public void GetAll_ShouldReturnsAllTasks()
    {
        var tasksBegin = _taskRepository.GetAll();

        _taskRepository.Create(new TaskEntityDto { TaskName = "Task 1", TaskDescription = "Description 1", TaskStatus = TaskEntityStatus.ToDo });
        _taskRepository.Create(new TaskEntityDto { TaskName = "Task 2", TaskDescription = "Description 2", TaskStatus = TaskEntityStatus.Completed });
        _taskRepository.Create(new TaskEntityDto { TaskName = "Task 3", TaskDescription = "Description 3", TaskStatus = TaskEntityStatus.Blocked });

        var tasksEnd = _taskRepository.GetAll();

        Assert.That(tasksEnd.Count(), Is.EqualTo(tasksBegin.Count() + 3));
    }

    [Test]
    public void Get_ShouldReturnTaskById()
    {
        var taskId = _taskRepository.Create(new TaskEntityDto { TaskName = "Task 1", TaskDescription = "Description 1", TaskStatus = TaskEntityStatus.ToDo });

        var task = _taskRepository.Get(taskId);

        Assert.That(task.TaskId, Is.EqualTo(taskId));
    }

    [Test]
    public void Update_ShouldUpdatedTask()
    {
        var taskId = _taskRepository.Create(new TaskEntityDto { TaskName = "Task 1", TaskDescription = "Description 1", TaskStatus = TaskEntityStatus.ToDo });

        var taskName = "Task 33";
        var taskDescription = "Description 33";
        var taskStatus = TaskEntityStatus.Completed;

        var updatedTask = new TaskEntityDto { TaskId = taskId, TaskName = taskName, TaskDescription = taskDescription, TaskStatus = taskStatus };
        _taskRepository.Update(updatedTask);

        var task = _taskRepository.Get(taskId);
        Assert.Multiple(() =>
        {
            Assert.That(updatedTask.TaskName, Is.EqualTo(taskName));
            Assert.That(updatedTask.TaskDescription, Is.EqualTo(taskDescription));
            Assert.That(updatedTask.TaskStatus, Is.EqualTo(taskStatus));
        });
    }

    [Test]
    public void Remove_ShouldDeleteTask()
    {
        var taskId = _taskRepository.Create(new TaskEntityDto { TaskName = "Task 1", TaskDescription = "Description 1", TaskStatus = TaskEntityStatus.ToDo });

        var tasksBegin = _taskRepository.GetAll();

        _taskRepository.Remove(taskId);

        var tasksEnd = _taskRepository.GetAll();

        Assert.That(tasksEnd.Count(), Is.EqualTo(tasksBegin.Count() - 1));
    }

}