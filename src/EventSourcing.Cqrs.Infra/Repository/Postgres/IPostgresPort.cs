using EventSourcingCqrs.Core.Todos.Aggregates;

namespace EventSourcing.Cqrs.Infra.Repository.Postgres;

internal interface IPostgresPort
{
    Task Save(Todo todo, Func<Task> beforeCommit = null);
    Task Update(Todo todo, Func<Task> beforeCommit = null);
    Task<Todo> GetById(Guid id);
    Task<IEnumerable<Todo>> GetAll();
}