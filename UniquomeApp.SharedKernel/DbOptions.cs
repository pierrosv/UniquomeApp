namespace UniquomeApp.SharedKernel;

public class DbOptions
{
    public string DbProvider { get; set; }
    public string MainConnectionString { get; set; }
    public string SecurityConnectionString { get; set; }
}