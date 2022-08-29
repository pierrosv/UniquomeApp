using UniquomeApp.Application.Mappings;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.ApplicationUsers.Queries;

public class OrganismVm : RootVm, IMapFrom<Organism>, IMapTo<Organism>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}