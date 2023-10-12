using TestSQLite8.ViewModel;

namespace TestSQLite8;

public partial class MainPage : ContentPage
{
    public MainPage(BookViewModel bookViewModel)
    {
        InitializeComponent();
        BindingContext = bookViewModel;
    }
}


