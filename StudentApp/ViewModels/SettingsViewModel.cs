using Microsoft.AspNetCore.Components;
using StudentApp.Services;

namespace StudentApp.ViewModels;



public class SettingsViewModel : BaseViewModel
{
    private ILocalStorageService _localStorageService;
    private NavigationManager _navigationManager;
    
    public SettingsViewModel(ILocalStorageService localStorageService, NavigationManager navigationManager)
    {
        _localStorageService = localStorageService;
        _navigationManager = navigationManager;
    }


    public async Task LogoutAsync()
    {
        await _localStorageService.RemoveItemAsync("token");
        
        _navigationManager.NavigateTo("/login");
    }
}