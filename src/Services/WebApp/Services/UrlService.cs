namespace WebApp.Services;

public interface IUrlService
{
    public string PostAutenticate();
    public string GetTasks();
}

public class UrlService : IUrlService
{

    public const string AutenticateUrl = "http://localhost:9000/api/Account/authenticate";
    public const string GetTasksUrl = "http://localhost:9001/api/tasks";

    public string PostAutenticate()
    {
        return AutenticateUrl;
    }

    public string GetTasks()
    {
        return GetTasksUrl;
    }
}