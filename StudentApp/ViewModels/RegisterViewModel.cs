using StudentApp.Services;
using WebAPI.Domain.Models;

namespace StudentApp.ViewModels;

public class RegisterViewModel : BaseViewModel
{
    private IUserService _userService;
    public UserInfo User = new();

    public RegisterViewModel(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> RegisterUserAsync()
    {
        return await _userService.RegisterUserAsync(User);
    }
}