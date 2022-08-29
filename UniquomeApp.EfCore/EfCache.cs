using UniquomeApp.SharedKernel;

namespace UniquomeApp.EfCore;

public static class EfCache
{
    public static DbConnectionOptions ConnectionOptions { get; set; } = default!;
}