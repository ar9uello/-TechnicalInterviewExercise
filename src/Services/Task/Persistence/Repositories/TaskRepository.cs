using Application.Dtos;
using Application.Interfaces.Persistence;
using Domain.Enums;
using System.Data.SqlClient;

namespace Persistence.Repositories;

public class TaskRepository : Repository, ITaskRepository
{
    public TaskRepository(SqlConnection context, SqlTransaction transaction)
    {
        Context = context;
        Transaction = transaction;
    }

    public List<TaskEntityDto> GetAll()
    {
        var result = new List<TaskEntityDto>();

        var command = CreateCommand("SELECT * FROM task");

        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                result.Add(new TaskEntityDto
                {
                    TaskId = Convert.ToInt32(reader["TaskId"]),
                    TaskName = Convert.ToString(reader["TaskName"]) ?? "",
                    TaskDescription = Convert.ToString(reader["TaskDescription"]) ?? "",
                    TaskStatus = (TaskEntityStatus)Enum.Parse(typeof(TaskEntityStatus), Convert.ToString(reader["TaskStatus"]) ?? nameof(TaskEntityStatus.ToDo))
                });
            }
        }

        return result;
    }

    public TaskEntityDto Get(int id)
    {
        var command = CreateCommand("SELECT * FROM Task WHERE TaskId = @TaskId");
        command.Parameters.AddWithValue("@TaskId", id);

        using (var reader = command.ExecuteReader())
        {
            reader.Read();

            return new TaskEntityDto
            {
                TaskId = Convert.ToInt32(reader["TaskId"]),
                TaskName = Convert.ToString(reader["TaskName"]) ?? "",
                TaskDescription = Convert.ToString(reader["TaskDescription"]) ?? "",
                TaskStatus = (TaskEntityStatus)Enum.Parse(typeof(TaskEntityStatus), Convert.ToString(reader["TaskStatus"]) ?? nameof(TaskEntityStatus.ToDo))
            };
        };
    }

    public int Create(TaskEntityDto model)
    {
        var query = "INSERT INTO Task (TaskName, TaskDescription, TaskStatus) VALUES (@TaskName, @TaskDescription, @TaskStatus); SELECT SCOPE_IDENTITY();";
        var command = CreateCommand(query);

        command.Parameters.AddWithValue("@TaskName", model.TaskName);
        command.Parameters.AddWithValue("@TaskDescription", model.TaskDescription);
        command.Parameters.AddWithValue("@TaskStatus", model.TaskStatus);

        return Convert.ToInt32(command.ExecuteScalar());
    }

    public void Update(TaskEntityDto model)
    {
        var query = "UPDATE Task set TaskName = @TaskName, TaskDescription = @TaskDescription, TaskStatus = @TaskStatus WHERE TaskId = @TaskId";
        var command = CreateCommand(query);

        command.Parameters.AddWithValue("@TaskId", model.TaskId);
        command.Parameters.AddWithValue("@TaskName", model.TaskName);
        command.Parameters.AddWithValue("@TaskDescription", model.TaskDescription);
        if (model.TaskStatus != null) command.Parameters.AddWithValue("@TaskStatus", model.TaskStatus);

        command.ExecuteNonQuery();
    }

    public void Remove(int taskId)
    {
        var command = CreateCommand("DELETE FROM Task WHERE TaskId = @TaskId");
        command.Parameters.AddWithValue("@TaskId", taskId);

        command.ExecuteNonQuery();
    }

}
