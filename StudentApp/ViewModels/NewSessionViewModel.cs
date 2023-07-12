using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using StudentApp.Interfaces;
using WebAPI.Domain.Models;

namespace StudentApp.ViewModels;

public class NewSessionViewModel : BaseViewModel
{
    private NavigationManager _navigationManager;
    private IPracticeSessionService _practiceSessionService;
    private Stopwatch CurrentFocusPointStopwatch { get; set; } = new();
    public List<FocusPoint> FocusPoints { get; set; } = new();
    public PracticeSession CurrentSession { get; set; } = new();
    public TimeSpan CurrentElapsedSession => GetTotalSessionTime();

    public NewSessionViewModel(IPracticeSessionService practiceSessionService, NavigationManager navigationManager)
    {
        _practiceSessionService = practiceSessionService;
        _navigationManager = navigationManager;
    }

    private TimeSpan GetTotalSessionTime()
    {
        if (FocusPoints.Count() is 0)
        {
            return CurrentFocusPointStopwatch.Elapsed;
        }
        var time = CurrentFocusPointStopwatch.Elapsed;
        foreach (var focusPoint in FocusPoints)
        {
            time += focusPoint.Length;
        }

        return time;
    }

    public bool IsRunningCurrentFocusPoint => CurrentFocusPointStopwatch.IsRunning;
    public void Start()
    {
        
        CurrentFocusPointStopwatch.Start();
    }
    
    public void Stop()
    {
        CurrentFocusPointStopwatch.Stop();
    }

    public void StartNewFocusPoint()
    {
        FocusPoints.Add(new FocusPoint(){Length = CurrentFocusPointStopwatch.Elapsed});
        
        CurrentFocusPointStopwatch.Restart();
    }

    public void FinalizeNewPracticeSession()
    {
        foreach (var i in FocusPoints)
        {
            if (i.Title is null)
            {
                i.Title = "Focus Point Number " + (FocusPoints.IndexOf(i) + 1);
            }
        }
        
        CurrentSession.FocusPoints = FocusPoints;
        CurrentSession.Date = DateTime.Today;
        CurrentSession.Length = GetTotalSessionTime();        
        
        

        _practiceSessionService.CreateNewPracticeSession(CurrentSession);

        
        
        _navigationManager.NavigateTo("/manage-sessions");
    }

    public void InitializeSession()
    {
        FocusPoints = new();
        CurrentSession = new();
        CurrentFocusPointStopwatch = new();
    }
}