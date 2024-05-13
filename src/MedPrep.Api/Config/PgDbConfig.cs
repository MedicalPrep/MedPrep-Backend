namespace MedPrep.Api.Config;

public class PgDbConfig
{
    public static string Name { get; } = "PgDbConfig";
    public required string Host { get; set; }
    public required string Database { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Port { get; set; }
    public string ConnectionString =>
        $"Username={this.Username}; Password={this.Password}; Host={this.Host}; Database={this.Database}; Port={this.Port}";
}
