using CleanTodo.Domain.Common;

namespace CleanTodo.Domain.Entities;

public sealed class TodoItem : BaseEntity<Guid>
{
    public string Title { get; private set; }
    public bool IsDone { get; private set; }

    public TodoItem(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        Id = Guid.NewGuid();
        Title = title.Trim();
    }

    public void MarkDone() => IsDone = true;
}
