using Domain.Common;

namespace Domain.Entities;

public class Post : AuditableEntity
{
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public string Content { get; private set; }
    private Post() { }

    public static Post Create(Guid userId, string content = null)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId inválido", nameof(userId));
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Content no puede estar vacío", nameof(content));
        return new Post
        {
            UserId = userId,
            Content = content
        };
    }
}
