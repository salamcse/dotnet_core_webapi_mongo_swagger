using CoreDotNetToken.Models;
using MongoDB.Driver;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(string connectionString)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase("StudentDB");
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

    internal IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}
