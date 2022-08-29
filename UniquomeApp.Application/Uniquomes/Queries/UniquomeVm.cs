using NodaTime;
using UniquomeApp.Application.Mappings;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Uniquomes.Queries;

public class UniquomeVm : RootVm, IMapFrom<Uniquome>, IMapTo<Uniquome>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Version { get; set; } = default!;
    public Instant CreationDate { get; set; }
}