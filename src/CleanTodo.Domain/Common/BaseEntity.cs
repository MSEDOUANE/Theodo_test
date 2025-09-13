namespace CleanTodo.Domain.Common;

public abstract class BaseEntity<TId>
{
    public TId Id { get; protected set; } = default!;
    public DateTime CreatedAtUtc { get; private set; }

    protected BaseEntity()
    {
        CreatedAtUtc = DateTime.UtcNow;
    }
}