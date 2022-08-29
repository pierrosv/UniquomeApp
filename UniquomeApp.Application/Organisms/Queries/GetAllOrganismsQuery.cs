using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Organisms.Queries;

public class GetAllOrganismsQuery : IRequest<IList<OrganismVm>>
{
    internal class GetAllOrganismsHandler : IRequestHandler<GetAllOrganismsQuery, IList<OrganismVm>>
    {
        private readonly IRepositoryBase<Organism> _repo;
        private readonly IMapper _mapper;

        public GetAllOrganismsHandler(IRepositoryBase<Organism> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IList<OrganismVm>> Handle(GetAllOrganismsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repo.ListAsync(cancellationToken);
            return _mapper.Map<List<Organism>, List<OrganismVm>>(entities.ToList());
        }
    }
}