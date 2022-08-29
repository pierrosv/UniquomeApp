using UniquomeApp.Domain;

namespace UniquomeApp.EfCore.Repos
{
    public class UniquomeRepository : AisExtendedExtendedEfRepo<Uniquome>, IUniquomeRepository
    {
        public UniquomeRepository(AisDbContext context) : base(context)
        {
        }
    }
}
