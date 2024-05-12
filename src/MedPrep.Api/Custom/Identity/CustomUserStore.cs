namespace MedPrep.Api.Custom.Identity;

using MedPrep.Api.Context;
using MedPrep.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class CustomUserStore : UserStore<User, Role, MedPrepContext, Guid>
{
    public CustomUserStore(MedPrepContext context)
        : base(context) => this.AutoSaveChanges = false;

    public CustomUserStore(MedPrepContext context, IdentityErrorDescriber? describer = null)
        : base(context, describer) => this.AutoSaveChanges = false;
}
