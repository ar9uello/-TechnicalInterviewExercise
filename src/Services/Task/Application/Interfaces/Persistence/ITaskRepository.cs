using Domain.Entities;

namespace Application.Interfaces.Persistence;

public interface ITaskRepository
{
    IEnumerable<TaskEntity> GetAll();

    TaskEntity Get(int id);

    int Create(TaskEntity model);

    void Update(TaskEntity model);

    void Remove(int id);
}
