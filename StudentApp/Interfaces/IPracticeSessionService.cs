using WebAPI.Domain.Models;

namespace StudentApp.Interfaces;

public interface IPracticeSessionService
{
    Task CreateNewPracticeSessionAsync(PracticeSession practiceSession);
    Task<List<PracticeSession>?> GetPracticeSessionsAsync();

    Task UpsertPracticeSessionAsync(PracticeSession practiceSession);
}