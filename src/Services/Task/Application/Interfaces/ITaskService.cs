using Application.Dtos;
using Application.ViewModels;

namespace Application.Interfaces;

public interface ITaskService
{
    List<GetTaskVm> GetAll();
    GetTaskVm GetById(int id);
    int Create(CreateTaskVm vm);
    void Update(UpdateTaskVm vm);
    void Delete(int id);
}