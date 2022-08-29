using NodaTime;
using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.Domain;

public class NewsletterRegistration : SimpleEntity
{
    public string Email { get; set; } = default!;
    public Instant AcceptedDate { get; set; }
    public Instant? RemovedDate { get; set; }
}