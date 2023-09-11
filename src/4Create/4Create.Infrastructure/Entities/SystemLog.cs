namespace _4Create.Infrastructure.Entities;

public class SystemLog
{
    public Guid Id { get; set; }
    public string ResourceType { get; set; } = null!;
    public Guid ResourceId { get; set; }
    public string EventName { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public object ChangeSet { get; set; } = null!;
    public string Comment { get; set; } = null!;
}
