using Microsoft.AspNetCore.Identity;

namespace Entities;

public class Role : IdentityRole<Guid>, IBaseEntity
{
    public string? CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public string? LastUpdateBy { get; set; }
    public DateTime? LastUpdateDate { get; set; }

    public string Description { get; set; } = string.Empty;
    public List<IdentityRoleClaim<Guid>> Claims { get; set; } = new();

    public Role()
    {
    }

    public Role(string name) : base(name)
    {
    }

    public override string ToString()
    {
        return Name;
    }
}
