using Newtonsoft.Json.Serialization;
using StudentApp.Services;
using WebAPI.Domain.Models;

namespace StudentApp.ViewModels;

public class LoginViewModel : BaseViewModel
{
    public UserInfo User { get; set; } = new();
    private readonly IUserService _userService;

    public LoginViewModel(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> TryLoginAsync()
    {
        return await _userService.LoginAsync(User);
    }
}