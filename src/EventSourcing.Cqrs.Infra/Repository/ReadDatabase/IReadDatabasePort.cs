using EventSourcingCqrs.Core.Todos.Aggregates;

namespace EventSourcing.Cqrs.Infra.Repository.ReadDatabase;

internal interface IReadDatabasePort
{
    Task Save(Todo todo, Func<Task> beforeCommit = null);
    Task Update(Todo todo, Func<Task> beforeCommit = null);
    Task<Todo> GetById(Guid id);
    Task<IEnumerable<Todo>> GetAll();
}