using UniquomeApp.Domain.Base;
using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.Application;

public interface IUniquomeRepoFactory
{
    IUniquomeExtendedRepository<TEntity> GetRepository<TEntity>() where TEntity : DomainRootEntity<long>;
    // IVesselPositionRepo GetVesselPositionRepo();
}