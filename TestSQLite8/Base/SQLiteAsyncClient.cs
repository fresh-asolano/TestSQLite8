using SQLite;

namespace TestSQLite8.Base;

public class SQLiteAsyncClient
{
    private SQLiteAsyncConnection connection;

    public SQLiteAsyncClient(string dbPath)
    {
        connection = new(dbPath);
        connection.CreateTableAsync<Book>().Wait();
    }

    public async Task SaveValueAsync<T>(T value, bool overrideIfExist = true)
        where T : IKeyObject, new()
    {
        var item = await connection.FindAsync<T>(value.Id);

        if (item is null)
            await connection.InsertAsync(value);
        else
            if (overrideIfExist)
            await connection.UpdateAsync(value);
        else
            throw new Exception($"Element with Id {value.Id} already exists in database");

    }

    public async Task DeleteValueAsync<T>(string id)
        where T : IKeyObject, new()
        => await connection.DeleteAsync<T>(id);

    public async Task<List<T>> GetAllValuesAsync<T>()
        where T : IKeyObject, new()
        => await connection.Table<T>().ToListAsync();
}