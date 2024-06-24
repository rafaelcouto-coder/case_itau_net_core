namespace CaseItau.Domain.Abstractions;

public abstract class Entity<TEntityId> : IEntity
{

    private readonly List<IDomainEvent> _domainEvents = [];

    protected Entity(TEntityId code) => Code = code;

    protected Entity()
    {
        Code = default!;
    }

    public TEntityId Code { get; init; }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return [.. _domainEvents];
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent ev)
    {
        _domainEvents.Add(ev);
    }
}
