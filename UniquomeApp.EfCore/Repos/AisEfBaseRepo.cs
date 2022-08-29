using Ardalis.Specification.EntityFrameworkCore;

namespace UniquomeApp.EfCore.Repos;

public class AisEfBaseRepo<T> : RepositoryBase<T> where T : class
{
    public AisEfBaseRepo(AisDbContext context) : base(context)
    {
    }
}