using StudentApp.Services;
using WebAPI.Domain.Models;

namespace StudentApp.ViewModels;

public class RegisterViewModel : BaseViewModel
{
    private IAuthenticationService _authenticationService;
    public UserInfo User = new();

    public RegisterViewModel(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<bool> RegisterUserAsync()
    {
        return await _authenticationService.RegisterUserAsync(User);
    }
}