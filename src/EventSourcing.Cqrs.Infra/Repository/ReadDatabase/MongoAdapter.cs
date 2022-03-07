using EventSourcingCqrs.Core.Todos.Aggregates;
using MongoDB.Driver;

namespace EventSourcing.Cqrs.Infra.Repository.ReadDatabase;

public class MongoAdapter: IReadDatabasePort
{
    private readonly IMongoCollection<Todo> _collection;
    private const string CollectionName = "Todos";

    public MongoAdapter(IMongoDatabase database)
    {
        _collection = database.GetCollection<Todo>(CollectionName);
    }
    
    public async Task Save(Todo todo, Func<Task> beforeCommit)
    {
        await _collection.InsertOneAsync(todo);
        await ExecuteOrRollback(todo.Id, beforeCommit);
    }

    public async Task Update(Todo todo, Func<Task> beforeCommit)
    {
        var definition = new ObjectUpdateDefinition<Todo>(todo);
        await _collection.FindOneAndUpdateAsync(e => e.Id == todo.Id, definition);
        await ExecuteOrRollback(todo.Id, beforeCommit);
    }

    private async Task ExecuteOrRollback(Guid id, Func<Task> beforeCommit)
    {
        try
        {
            await beforeCommit();
        }
        catch (Exception)
        {
            await _collection.FindOneAndDeleteAsync(e => e.Id == id);
            throw;
        }
    }

    public async Task<Todo> GetById(Guid id) =>
        await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();

    public async Task<IEnumerable<Todo>> GetAll() =>  
        (await _collection.FindAsync(e => true)).ToList();
}