using StudentApp.Interfaces;
using WebAPI.Domain.Models;

namespace StudentApp.Services;

public class MockPracticeSessionService : IPracticeSessionService
{
    public Task CreateNewPracticeSessionAsync(PracticeSession practiceSession)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PracticeSession>?> GetPracticeSessionsAsync()
    {
         List<PracticeSession> practiceSessions = new List<PracticeSession>
        {
            new PracticeSession
            {
                userId = "x",
                Title = "Practice Session 1",
                Length = TimeSpan.FromMinutes(30),
                Date = DateTime.Now.AddDays(0),
                FocusPoints = new List<FocusPoint>
                {
                    new FocusPoint { Title = "Focus Point 1", Length = TimeSpan.FromMinutes(10) }
                }
            },
            new PracticeSession
            {
                userId = "x",
                Title = "Practice Session 2",
                Length = TimeSpan.FromMinutes(45),
                Date = DateTime.Now.AddDays(1),
                FocusPoints = new List<FocusPoint>
                {
                    new FocusPoint { Title = "Focus Point 1", Length = TimeSpan.FromMinutes(15) },
                    new FocusPoint { Title = "Focus Point 2", Length = TimeSpan.FromMinutes(20) }
                }
            },
            new PracticeSession
            {
                userId = "x",
                Title = "Practice Session 3",
                Length = TimeSpan.FromMinutes(60),
                Date = DateTime.Now.AddDays(2),
                FocusPoints = new List<FocusPoint>
                {
                    new FocusPoint { Title = "Focus Point 1", Length = TimeSpan.FromMinutes(10) },
                    new FocusPoint { Title = "Focus Point 2", Length = TimeSpan.FromMinutes(15) },
                    new FocusPoint { Title = "Focus Point 3", Length = TimeSpan.FromMinutes(20) }
                }
            },
            new PracticeSession
            {
                userId = "x",
                Title = "Practice Session 4",
                Length = TimeSpan.FromMinutes(40),
                Date = DateTime.Now.AddDays(3),
                FocusPoints = new List<FocusPoint>
                {
                    new FocusPoint { Title = "Focus Point 1", Length = TimeSpan.FromMinutes(12) },
                    new FocusPoint { Title = "Focus Point 2", Length = TimeSpan.FromMinutes(18) }
                }
            },
            new PracticeSession
            {
                userId = "x",
                Title = "Practice Session 5",
                Length = TimeSpan.FromMinutes(55),
                Date = DateTime.Now.AddDays(4),
                FocusPoints = new List<FocusPoint>
                {
                    new FocusPoint { Title = "Focus Point 1", Length = TimeSpan.FromMinutes(8) },
                    new FocusPoint { Title = "Focus Point 2", Length = TimeSpan.FromMinutes(14) },
                    new FocusPoint { Title = "Focus Point 3", Length = TimeSpan.FromMinutes(22) }
                }
            },
            new PracticeSession
            {
                userId = "x",
                Title = "Practice Session 6",
                Length = TimeSpan.FromMinutes(50),
                Date = DateTime.Now.AddDays(5),
                FocusPoints = new List<FocusPoint>
                {
                    new FocusPoint { Title = "Focus Point 1", Length = TimeSpan.FromMinutes(10) },
                    new FocusPoint { Title = "Focus Point 2", Length = TimeSpan.FromMinutes(15) }
                }
            },
            new PracticeSession
            {
                userId = "x",
                Title = "Practice Session 7",
                Length = TimeSpan.FromMinutes(35),
                Date = DateTime.Now.AddDays(6),
                FocusPoints = new List<FocusPoint>
                {
                    new FocusPoint { Title = "Focus Point 1", Length = TimeSpan.FromMinutes(8) },
                    new FocusPoint { Title = "Focus Point 2", Length = TimeSpan.FromMinutes(12) },
                    new FocusPoint { Title = "Focus Point 3", Length = TimeSpan.FromMinutes(15) }
                }
            },
            new PracticeSession
            {
                userId = "x",
                Title = "Practice Session 8",
                Length = TimeSpan.FromMinutes(65),
                Date = DateTime.Now.AddDays(7),
                FocusPoints = new List<FocusPoint>
                {
                    new FocusPoint { Title = "Focus Point 1", Length = TimeSpan.FromMinutes(20) },
                    new FocusPoint { Title = "Focus Point 2", Length = TimeSpan.FromMinutes(30) },
                    new FocusPoint { Title = "Focus Point 3", Length = TimeSpan.FromMinutes(15) }
                }
            },
            new PracticeSession
            {
                userId = "x",
                Title = "Practice Session 9",
                Length = TimeSpan.FromMinutes(55),
                Date = DateTime.Now.AddDays(8),
                FocusPoints = new List<FocusPoint>
                {
                    new FocusPoint { Title = "Focus Point 1", Length = TimeSpan.FromMinutes(18) },
                    new FocusPoint { Title = "Focus Point 2", Length = TimeSpan.FromMinutes(22) }
                }
            },
            new PracticeSession
            {
                userId = "x",
                Title = "Practice Session 10",
                Length = TimeSpan.FromMinutes(40),
                Date = DateTime.Now.AddDays(9),
                FocusPoints = new List<FocusPoint>
                {
                    new FocusPoint { Title = "Focus Point 1", Length = TimeSpan.FromMinutes(15) },
                    new FocusPoint { Title = "Focus Point 2", Length = TimeSpan.FromMinutes(10) }
                }
            }
        };

         return practiceSessions;
    }

    public Task UpsertPracticeSessionAsync(PracticeSession practiceSession)
    {
        throw new NotImplementedException();
    }
}

public class PracticeSessionService : IPracticeSessionService
{
    private ILocalStorageService _localStorageService;
    private IHttpService _httpService;

    public PracticeSessionService(ILocalStorageService localStorageService, IHttpService httpService)
    {
        _localStorageService = localStorageService;
        _httpService = httpService;
    }

    public async Task CreateNewPracticeSessionAsync(PracticeSession practiceSession)
    {
        var token = await _localStorageService.GetItemAsync<AuthenticationResponse>("token");

        practiceSession.userId = token.userId;

        await _httpService.PostNewPracticeSessionAsync(practiceSession);
    }

    public async Task<List<PracticeSession>?> GetPracticeSessionsAsync()
    {
        var token = await _localStorageService.GetItemAsync<AuthenticationResponse>("token");

        var header = token.accessToken;

        var userId = token.userId;

        var response = await _httpService.GetPracticeSessionsByUserIdAsync(userId, header);

        return response;
    }

    public async Task UpsertPracticeSessionAsync(PracticeSession practiceSession)
    {
        await _httpService.UpsertPracticeSessionAsync(practiceSession);
    }
}