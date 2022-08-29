using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.Domain;

public class Organism : SimpleEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}