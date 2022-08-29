using UniquomeApp.Application.Mappings;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Proteomes.Queries;

public class ProteomeVm : RootVm, IMapFrom<Proteome>, IMapTo<Proteome>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Version { get; set; } = default!;
}