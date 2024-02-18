using Application.Dtos;

namespace Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllAsync();
    Task<TaskDto> GetByIdAsync(int id);
    Task<TaskDto> AddAsync(TaskDto taskDto);
    Task<TaskDto> UpdateAsync(TaskDto taskDto);
    Task<TaskDto> DeleteAsync(int id);
}