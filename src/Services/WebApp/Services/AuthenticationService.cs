using Microsoft.AspNetCore.Components;
using WebApp.Models;

namespace WebApp.Services;

public interface IAuthenticationService
{
    User? User { get; }
    Task Initialize();
    Task Login(string username, string password);
    Task Logout();
}

public class AuthenticationService(IHttpService httpService, NavigationManager navigationManager, ILocalStorageService localStorageService, IUrlService urlService) : IAuthenticationService
{
    private readonly IHttpService _httpService = httpService;
    private readonly NavigationManager _navigationManager = navigationManager;
    private readonly ILocalStorageService _localStorageService = localStorageService;
    private readonly IUrlService _urlService = urlService;
    public User? User { get; set; }

    public async Task Initialize()
    {
        User = await _localStorageService.GetItem<User>("user");
    }

    public async Task Login(string username, string password)
    {
        User = await _httpService.Post<User>(_urlService.PostAutenticate(), new { username, password });
        await _localStorageService.SetItem("user", User);
    }

    public async Task Logout()
    {
        User = null;
        await _localStorageService.RemoveItem("user");
        _navigationManager.NavigateTo("login");
    }

}