namespace Application.Interfaces.Persistence;

public interface IUnitOfWorkAdapter : IDisposable
{
    IUnitOfWorkRepository Repositories { get; }
    void SaveChanges();
}
