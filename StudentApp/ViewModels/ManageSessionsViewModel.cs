using StudentApp.Interfaces;
using StudentApp.Services;
using WebAPI.Domain.Models;

namespace StudentApp.ViewModels;



public class ManageSessionsViewModel : BaseViewModel
{
    private IPracticeSessionService _practiceSessionService;
    public List<PracticeSession>? PracticeSessions;
    public PracticeSession? SelectedPracticeSession { get; set; }
    
    public ManageSessionsViewModel(IPracticeSessionService practiceSessionService)
    {
        _practiceSessionService = practiceSessionService;
    }

    public async Task SetPracticeSessions()
    {
        PracticeSessions = await _practiceSessionService.GetPracticeSessionsAsync();
    }
}