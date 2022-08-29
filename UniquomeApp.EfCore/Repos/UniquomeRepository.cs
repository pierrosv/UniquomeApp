using UniquomeApp.Domain;

namespace UniquomeApp.EfCore.Repos;

public class UniquomeRepository : UniquomeExtendedExtendedEfRepo<Uniquome>, IUniquomeRepository
{
    public UniquomeRepository(UniquomeDbContext context) : base(context)
    {
    }
}