using UniquomeApp.Application.Mappings;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.NewsletterRegistrations.Queries;

public class NewsletterRegistrationVm : RootVm, IMapFrom<NewsletterRegistration>, IMapTo<NewsletterRegistration>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}