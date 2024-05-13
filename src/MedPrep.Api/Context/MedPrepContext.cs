namespace MedPrep.Api.Context;

using MedPrep.Api.Config;
using MedPrep.Api.Models;
using MedPrep.Api.Models.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class MedPrepContext(IOptions<PgDbConfig> options) : IdentityDbContext<Account, Role, Guid>()
{
    private readonly PgDbConfig config = options.Value;
    public DbSet<Playlist> Playlist { get; set; } = null!;
    public DbSet<RefreshToken> RefreshToken { get; set; } = null!;
    public DbSet<CourseModule> CourseModule { get; set; } = null!;
    public DbSet<Video> Video { get; set; } = null!;
    public DbSet<User> User { get; set; } = null!;
    public DbSet<License> License { get; set; } = null!;
    public DbSet<SubtitleSource> SubtitleSource { get; set; } = null!;
    public DbSet<VideoSource> VideoSource { get; set; } = null!;
    public DbSet<Teacher> Teacher { get; set; } = null!;
    public DbSet<Account> Account { get; set; } = null!;
    public DbSet<Role> Role { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(this.config.ConnectionString);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Identity renamed tables
        _ = builder.Entity<IdentityUserToken<Guid>>().ToTable("AccountToken");
        _ = builder.Entity<IdentityUserRole<Guid>>().ToTable("AccountRole");
        _ = builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaim");
        _ = builder.Entity<IdentityUserClaim<Guid>>().ToTable("AccountClaim");
        _ = builder.Entity<IdentityUserLogin<Guid>>().ToTable("AccountLogin");

        // Soft delete filter
        _ = builder
            .Entity<Account>()
            .HasDiscriminator(a => a.AccountType)
            .HasValue<Teacher>(AccountType.Teacher)
            .HasValue<User>(AccountType.User);
        // _ = builder
        //     .Entity<Account>()
        //     .HasIndex(user => user.IsDeleted)
        //     .HasFilter("AspNetUsers.IsDeleted = 0");
        _ = builder.Entity<Account>().HasQueryFilter(user => user.IsDeleted == false);

        // _ = builder
        //     .Entity<RefreshToken>()
        //     .HasIndex(refreshToken => refreshToken.IsDeleted)
        //     .HasFilter("IsDeleted = 0");
        _ = builder
            .Entity<RefreshToken>()
            .HasQueryFilter(refreshToken => refreshToken.IsDeleted == false);

        // _ = builder
        //     .Entity<Playlist>()
        //     .HasIndex(playlist => playlist.IsDeleted)
        //     .HasFilter("IsDeleted = 0");
        _ = builder.Entity<Playlist>().HasQueryFilter(playlist => playlist.IsDeleted == false);

        // _ = builder
        //     .Entity<CourseModule>()
        //     .HasIndex(courseModule => courseModule.IsDeleted)
        //     .HasFilter("IsDeleted = 0");
        _ = builder
            .Entity<CourseModule>()
            .HasQueryFilter(courseModule => courseModule.IsDeleted == false);

        // _ = builder.Entity<Video>().HasIndex(video =>
        // video.IsDeleted).HasFilter("IsDeleted = 0");
        _ = builder.Entity<Video>().HasQueryFilter(video => video.IsDeleted == false);

        // _ = builder
        //     .Entity<License>()
        //     .HasIndex(license => license.IsDeleted)
        //     .HasFilter("IsDeleted = 0");
        _ = builder.Entity<License>().HasQueryFilter(license => license.IsDeleted == false);

        // _ = builder
        //     .Entity<VideoSource>()
        //     .HasIndex(videoSource => videoSource.IsDeleted)
        //     .HasFilter("IsDeleted = 0");
        _ = builder
            .Entity<VideoSource>()
            .HasQueryFilter(videoSource => videoSource.IsDeleted == false);

        // _ = builder
        //     .Entity<SubtitleSource>()
        //     .HasIndex(subtitleSource => subtitleSource.IsDeleted)
        //     .HasFilter("IsDeleted = 0");
        _ = builder
            .Entity<SubtitleSource>()
            .HasQueryFilter(subtitleSource => subtitleSource.IsDeleted == false);

        base.OnModelCreating(builder);
    }
}
