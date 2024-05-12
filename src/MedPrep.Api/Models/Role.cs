namespace MedPrep.Api.Models;

using Microsoft.AspNetCore.Identity;

public class Role : IdentityRole<Guid>
{
    public Role() { }

    public Role(string name)
        : base(name) { }
}
