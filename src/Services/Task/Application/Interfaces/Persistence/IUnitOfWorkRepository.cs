namespace Application.Interfaces.Persistence;

public interface IUnitOfWorkRepository
{
    public ITaskRepository TaskRepository { get; }
}
