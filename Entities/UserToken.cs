namespace Entities;

public class UserToken : BaseEntity
{
    public List<string> Tokens { get; set; } = new();
}