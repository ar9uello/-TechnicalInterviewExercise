namespace Application.Interfaces.Persistence;

public interface IUnitOfWork
{
    IUnitOfWorkAdapter Create();
}