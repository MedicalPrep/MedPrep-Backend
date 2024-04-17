namespace MedPrep.Api.Config;

public class PgDbConfig
{
    public static string Name { get; } = "PgDbConfig";
    public required string Host { get; set; }
    public required string Database { get; set; }
    public required string User { get; set; }
    public required string Password { get; set; }
    public string ConnectionString =>
        $"User={this.User} Password={this.Password} Host={this.Host} Database={this.Database}";
}
