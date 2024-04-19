namespace MedPrep.Api.Context;

using MedPrep.Api.Config;
using MedPrep.Api.Context.Interceptors;
using MedPrep.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class MedPrepContext(
    DbContextOptions<MedPrepContext> dbContextOptions,
    IOptions<PgDbConfig> options
) : DbContext(dbContextOptions)
{
    private readonly PgDbConfig config = options.Value;
    public DbSet<Playlist> Playlist { get; set; } = null!;
    public DbSet<CourseModule> CourseModule { get; set; } = null!;
    public DbSet<Video> Video { get; set; } = null!;
    public DbSet<User> User { get; set; } = null!;
    public DbSet<License> License { get; set; } = null!;
    public DbSet<SubtitleSource> SubtitleSource { get; set; } = null!;
    public DbSet<VideoSource> VideoSource { get; set; } = null!;
    public DbSet<Teacher> Teacher { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder
            .UseNpgsql(this.config.ConnectionString)
            .AddInterceptors(new SoftDeleteInterceptor());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder
            .Entity<Playlist>()
            .HasIndex(playlist => playlist.IsDeleted)
            .HasFilter("IsDeleted = 0");
        _ = modelBuilder.Entity<Playlist>().HasQueryFilter(playlist => playlist.IsDeleted == false);

        _ = modelBuilder
            .Entity<CourseModule>()
            .HasIndex(courseModule => courseModule.IsDeleted)
            .HasFilter("IsDeleted = 0");
        _ = modelBuilder
            .Entity<CourseModule>()
            .HasQueryFilter(courseModule => courseModule.IsDeleted == false);

        _ = modelBuilder
            .Entity<Video>()
            .HasIndex(video => video.IsDeleted)
            .HasFilter("IsDeleted = 0");
        _ = modelBuilder.Entity<Video>().HasQueryFilter(video => video.IsDeleted == false);

        _ = modelBuilder.Entity<User>().HasIndex(user => user.IsDeleted).HasFilter("IsDeleted = 0");
        _ = modelBuilder.Entity<User>().HasQueryFilter(user => user.IsDeleted == false);

        _ = modelBuilder
            .Entity<License>()
            .HasIndex(license => license.IsDeleted)
            .HasFilter("IsDeleted = 0");
        _ = modelBuilder.Entity<License>().HasQueryFilter(license => license.IsDeleted == false);

        _ = modelBuilder
            .Entity<Teacher>()
            .HasIndex(license => license.IsDeleted)
            .HasFilter("IsDeleted = 0");
        _ = modelBuilder.Entity<License>().HasQueryFilter(license => license.IsDeleted == false);

        _ = modelBuilder
            .Entity<VideoSource>()
            .HasIndex(videoSource => videoSource.IsDeleted)
            .HasFilter("IsDeleted = 0");
        _ = modelBuilder
            .Entity<VideoSource>()
            .HasQueryFilter(videoSource => videoSource.IsDeleted == false);

        _ = modelBuilder
            .Entity<SubtitleSource>()
            .HasIndex(subtitleSource => subtitleSource.IsDeleted)
            .HasFilter("IsDeleted = 0");
        _ = modelBuilder
            .Entity<SubtitleSource>()
            .HasQueryFilter(subtitleSource => subtitleSource.IsDeleted == false);

        base.OnModelCreating(modelBuilder);
    }
}
