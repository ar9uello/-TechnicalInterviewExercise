using Application.Dtos;

namespace Application.Interfaces;

public interface ITaskService
{
    List<TaskEntityDto> GetAll();
    TaskEntityDto GetById(int id);
    int Add(TaskEntityDto taskDto);
    void Update(TaskEntityDto taskDto);
    void Delete(int id);
}