using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using System.Text;
using WebApp.Models;

namespace WebApp.Services;

public interface IHttpService
{
    Task<T> Get<T>(string uri);
    Task<T> Post<T>(string uri, object value);
}

public class HttpService(
    HttpClient httpClient,
    NavigationManager navigationManager,
    ILocalStorageService localStorageService
    ) : IHttpService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly NavigationManager _navigationManager = navigationManager;
    private readonly ILocalStorageService _localStorageService = localStorageService;

    public async Task<T> Get<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        var result = await SendRequest<T>(request);
        return result == null ? throw new Exception("SendRequest<T> returned null") : result;
    }

    public async Task<T> Post<T>(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
        };
        var result = await SendRequest<T>(request);
        return result == null ? throw new Exception("SendRequest<T> returned null") : result;
    }

    // helper methods

    private async Task<T?> SendRequest<T>(HttpRequestMessage request)
    {
        var user = await _localStorageService.GetItem<User>("user");
        if (user != null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

        try
        {
            using var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout");
                return default;
            }
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                if (error != null) throw new Exception(error["message"]);
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return default;
    }
}