using NodaTime;
using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.Domain.Base;

public class AuditableEntity<TKey> : DomainRootEntity<TKey> where TKey : struct
{
    public string CreatedBy { get; set; } = default!;
    public Instant CreatedAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public Instant? LastModifiedAt { get; set; }
    public string? DeletedBy { get; set; }
    public Instant? DeletedAt { get; set; }
    public int RowVersion { get; set; }
}