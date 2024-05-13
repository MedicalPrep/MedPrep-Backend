namespace MedPrep.Api.Custom.Identity;

using MedPrep.Api.Context;
using MedPrep.Api.Models;
using MedPrep.Api.Models.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class CustomAccountStore : UserStore<Account, Role, MedPrepContext, Guid>
{
    public CustomAccountStore(MedPrepContext context)
        : base(context) => this.AutoSaveChanges = false;

    public CustomAccountStore(MedPrepContext context, IdentityErrorDescriber? describer = null)
        : base(context, describer) => this.AutoSaveChanges = false;
}
