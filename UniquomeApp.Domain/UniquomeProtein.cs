using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.Domain;

public class UniquomeProtein : SimpleEntity
{
    public Uniquome ForUniquome { get; set; }
    public long ForUniquomeId { get; set; }
    public Protein ForProtein { get; set; }
    public long ForProteinId { get; set; }
    public IList<Peptide> Peptides { get; set; } = new List<Peptide>();
}