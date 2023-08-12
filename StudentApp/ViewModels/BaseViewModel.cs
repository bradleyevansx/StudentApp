using StudentApp.Services;

namespace StudentApp.ViewModels;

public abstract class BaseViewModel
{
    protected bool _isBusy;

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (_isBusy != value)
            {
                _isBusy = value;
                // Raise PropertyChanged event if you need to notify UI about the change
            }
        }
    }

    public async Task<TResult> RunMethodAsync<TResult>(Func<Task<TResult>> arg)
    {
        IsBusy = true; 
        var result = await arg.Invoke();
        IsBusy = false;

        return result;
    }
}
