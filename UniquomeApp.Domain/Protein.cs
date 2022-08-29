using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.Domain;

public class Protein : SimpleEntity
{
    public Proteome InProteome { get; set; }
    public long InProteomeId { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Sequence { get; set; } = default!;
}