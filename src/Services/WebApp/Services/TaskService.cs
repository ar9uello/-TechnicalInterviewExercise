using WebApp.Models;

namespace WebApp.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskEntity>> GetAllTask();
}

public class TaskService(IHttpService httpService, IUrlService urlService) : ITaskService
{
    private readonly IHttpService _httpService = httpService;
    private readonly IUrlService _urlService = urlService;

    public async Task<IEnumerable<TaskEntity>> GetAllTask()
    {
        var response = await _httpService.Get<ICollection<TaskEntity>>(_urlService.GetTasks());
        return response ?? Enumerable.Empty<TaskEntity>();
    }
    
}