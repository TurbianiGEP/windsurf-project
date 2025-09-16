using DDDTemplate.Domain.Events;

namespace DDDTemplate.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
        bool IsDeleted { get; }
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void AddDomainEvent(IDomainEvent eventItem);
        void RemoveDomainEvent(IDomainEvent eventItem);
        void ClearDomainEvents();
        void MarkAsUpdated();
        void MarkAsDeleted();
    }
}
