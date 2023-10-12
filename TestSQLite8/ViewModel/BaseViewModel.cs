namespace TestSQLite8.ViewModel;

public class BaseViewModel : ObservableObject, IQueryAttributable

{
    public BaseViewModel()
    {
    }

    private bool isBusy;

    public bool IsBusy
    {
        get => isBusy;
        set => SetProperty(ref isBusy, value);
    }

    public virtual Task SaveAsync() => Task.CompletedTask;

    public virtual Task OnAppearing() => Task.CompletedTask;

    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {

    }
}