using System.Data;
using Dapper;
using EventSourcingCqrs.Core.Todos.Aggregates;

namespace EventSourcing.Cqrs.Infra.Repository.Postgres;

internal class PostgresAdapter : IPostgresPort
{
    private readonly IDbConnection _connection;

    public PostgresAdapter(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public async Task Save(Todo todo, Func<Task> beforeCommit = null)
    {
        const string query = 
            @"INSERT INTO todo (id, description, status) 
              VALUES (@Id, @Description, @Status)";

        using var transaction = _connection.BeginTransaction();
        await _connection.ExecuteAsync(query, todo, transaction);

        if (beforeCommit != null) 
             await beforeCommit();

        transaction.Commit();
    }
    
    public async Task Update(Todo todo, Func<Task> beforeCommit = null)
    {
        const string query = 
            @"UPDATE todo SET description = @Description, status = @Status 
              WHERE Id = @Id";

        using var transaction = _connection.BeginTransaction();
        await _connection.ExecuteAsync(query, todo, transaction);

        if (beforeCommit != null) 
            await beforeCommit();

        transaction.Commit();
    }
    
    public async Task<Todo> GetById(Guid id)
    {
        const string query = "SELECT * FROM todo WHERE Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<Todo>(query, new { Id = id });
    }
    
    public async Task<IEnumerable<Todo>> GetAll()
    {
        const string query = "SELECT * FROM todo";
        return await _connection.QueryAsync<Todo>(query);
    }
}