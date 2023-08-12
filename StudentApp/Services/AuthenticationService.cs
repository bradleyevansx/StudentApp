using System.Text.Json;
using StudentApp.Services.Interfaces;
using WebAPI.Domain.Models;

namespace StudentApp.Services;

public class AuthenticationService : IAuthenticationService
{
    private ILocalStorageService _localStorageService;
    private IHttpService _httpService;


    public AuthenticationService(ILocalStorageService localStorageService, IHttpService httpService)
    {
        _localStorageService = localStorageService;
        _httpService = httpService;
    }


    public async Task<bool> RegisterUserAsync(UserInfo newUser)
    {
        var authenticationResponse = await _httpService.RegisterUserAsync(newUser);
        var serializedResponse = JsonSerializer.Serialize(authenticationResponse);

        await _localStorageService.SetItemAsync("token", serializedResponse);

        if (authenticationResponse is not null) return true;
        return false;
    }

    public async Task<bool> TryRefreshAsync()
    {
        var authenticationResponse = await _localStorageService.GetItemAsync<AuthenticationResponse>("token");
        if (authenticationResponse is null) return false;

        var newAuthenticationResponse = await _httpService.TryAuthUserAsync(authenticationResponse.refreshTokenId);
        var serializedResponse = JsonSerializer.Serialize(newAuthenticationResponse);
        await _localStorageService.SetItemAsync("token", serializedResponse);
        return true;
    }
    public async Task<bool> LoginAsync(UserInfo user)
    {
        var authenticationResponse = await _httpService.TryAuthUserAsync(user);

        var serializedResponse = JsonSerializer.Serialize(authenticationResponse);

        await _localStorageService.SetItemAsync("token", serializedResponse);

        if (authenticationResponse is not null) return true;
        else return false;
    }
}