namespace UniquomeApp.SharedKernel.DomainCore;

public abstract class DomainRootEntity<TKey> where TKey : struct
{
    protected bool Equals(DomainRootEntity<TKey>? other)
    {
        if (other == null) return false;
        return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode()
    {
        //TODO: Find a better hashing ?
        return EqualityComparer<TKey>.Default.GetHashCode(Id);
    }

    public TKey Id { get; set; }

    public override bool Equals(object? obj)
    {
        var other = obj as DomainRootEntity<TKey>;

        if (ReferenceEquals(other, null))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        if (default(TKey).Equals(Id))
            return false;

        return Id.Equals(other.Id);
    }

    public static bool operator ==(DomainRootEntity<TKey>? a, DomainRootEntity<TKey>? b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;
        
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(DomainRootEntity<TKey>? a, DomainRootEntity<TKey>? b)
    {
        return !(a == b);
    }
    // public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
}