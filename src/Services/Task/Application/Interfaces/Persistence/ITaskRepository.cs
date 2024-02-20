using Application.Dtos;

namespace Application.Interfaces.Persistence;

public interface ITaskRepository
{
    List<TaskEntityDto> GetAll();

    TaskEntityDto GetById(int id);

    int Create(TaskEntityDto model);

    void Update(TaskEntityDto model);

    void Delete(int id);
}
