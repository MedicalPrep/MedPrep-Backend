namespace MedPrep.Api.Custom.Identity;

using MedPrep.Api.Context;
using MedPrep.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class CustomTeacherStore : UserStore<Teacher, Role, MedPrepContext, Guid>
{
    public CustomTeacherStore(MedPrepContext context)
        : base(context) => this.AutoSaveChanges = false;

    public CustomTeacherStore(MedPrepContext context, IdentityErrorDescriber? describer = null)
        : base(context, describer) => this.AutoSaveChanges = false;
}
