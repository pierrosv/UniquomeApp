using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.Domain;

public class Peptide : SimpleEntity
{
    public UniquomeProtein InUniquomeProtein { get; set; }
    public long InUniquomeProteinId { get; set; }
    public string Sequence { get; set; } = default!;
    public int FirstLocation { get; set; }
}