namespace UniquomeApp.SharedKernel;

public class DbConnectionOptions
{
    public string Name { get; set; } = default!;
    public int Timeout { get; set; }
    public string DbProvider { get; set; } = default!;
    public string ConnectionString { get; set; } = default!;
}