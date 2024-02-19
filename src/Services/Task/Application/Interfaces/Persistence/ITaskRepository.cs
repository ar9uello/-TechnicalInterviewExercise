using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.Persistence;

public interface ITaskRepository
{
    List<TaskEntityDto> GetAll();

    TaskEntityDto Get(int id);

    int Create(TaskEntityDto model);

    void Update(TaskEntityDto model);

    void Remove(int id);
}
