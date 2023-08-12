using WebAPI.Domain.Models;

namespace StudentApp.Services;

public interface IHttpService
{
    Task<AuthenticationResponse?> RegisterUserAsync(UserInfo newUser);
    public Task<AuthenticationResponse?> TryAuthUserAsync(string refreshTokenId);
    public Task<AuthenticationResponse?> TryAuthUserAsync(UserInfo newUser);
    Task<List<PracticeSession>?> GetPracticeSessionsByUserIdAsync(string userId, string accessToken);
    public Task PostNewPracticeSessionAsync(PracticeSession practiceSession);

    public Task UpsertPracticeSessionAsync(PracticeSession practiceSession);
}