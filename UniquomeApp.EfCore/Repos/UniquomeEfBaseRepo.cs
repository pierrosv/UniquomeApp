using Ardalis.Specification.EntityFrameworkCore;

namespace UniquomeApp.EfCore.Repos;

public class UniquomeEfBaseRepo<T> : RepositoryBase<T> where T : class
{
    public UniquomeEfBaseRepo(UniquomeDbContext context) : base(context)
    {
    }
}