using WebAPI.Domain.Models;

namespace StudentApp.Interfaces;

public interface IPracticeSessionService
{
    Task CreateNewPracticeSession(PracticeSession practiceSession);
    Task<List<PracticeSession>?> GetPracticeSessions();

    Task UpsertPracticeSession(PracticeSession practiceSession);
}