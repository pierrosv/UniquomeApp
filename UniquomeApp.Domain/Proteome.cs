using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.Domain;

public class Proteome : SimpleEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Version { get; set; } = default!;
    public IList<Protein> Proteins { get; set; } = new List<Protein>();
}