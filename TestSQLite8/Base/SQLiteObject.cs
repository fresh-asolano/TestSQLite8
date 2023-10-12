using SQLite;

namespace TestSQLite8.Base;

public class SQLiteObject : IKeyObject
{
    [PrimaryKey]
    public int Id { get; set; }
}