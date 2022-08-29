using NodaTime;
using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.Domain;

public class Uniquome : SimpleEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Version { get; set; } = default!;
    public Instant CreationDate { get; set; }
    public IList<UniquomeProtein> Proteins { get; set; } = new List<UniquomeProtein>();
}