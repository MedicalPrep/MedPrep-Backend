namespace MedPrep.Api.Context;

using MedPrep.Api.Config;
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
        optionsBuilder.UseNpgsql(this.config.ConnectionString);
}
