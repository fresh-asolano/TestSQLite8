using TestSQLite8.Base;

namespace TestSQLite8.ViewModel;

public class BookViewModel : BaseViewModel
{
    private readonly SQLiteAsyncClient _dbConnection;

    public BookViewModel(SQLiteAsyncClient dbConnection)
    {
        _dbConnection = dbConnection;
        SearchCommand = new(async () => await LoadAsync());
        SaveCommand = new(async () => await SaveAsync());
    }

    private async Task LoadAsync()
    {
        if (BookId != 0)
            BookModel = (await _dbConnection.GetAllValuesAsync<Book>()).FirstOrDefault(b => b.Id == bookId);
        else
            BookModel = (await _dbConnection.GetAllValuesAsync<Book>()).FirstOrDefault();

        if (BookModel is not null)
        {
            BookId = BookModel.Id;
            AuthorName = BookModel.AuthorName;
            BookName = BookModel.BookName;
        }
    }

    private async Task SaveAsync()
    {
        if (!IsBusy)
        {
            IsBusy = true;

            Book newBook = new() { Id = BookId, AuthorName = AuthorName, BookName = BookName };

            await _dbConnection.SaveValueAsync(newBook);

            await LoadAsync();

            IsBusy = false;
        }
    }

    private Book bookModel;

    public Book BookModel
    {
        get => bookModel;
        set => SetProperty(ref bookModel, value);
    }

    public Command SaveCommand { get; set; }

    public Command SearchCommand { get; set; }

    #region Properties

    private int bookId;

    public int BookId
    {
        get => bookId;
        set => SetProperty(ref bookId, value);
    }

    private string authorName;

    public string AuthorName
    {
        get => authorName;
        set => SetProperty(ref authorName, value);
    }

    private string bookName;

    public string BookName
    {
        get => bookName;
        set => SetProperty(ref bookName, value);
    }
    #endregion
}