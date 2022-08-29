using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Application.Common.Exceptions;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.ApplicationUsers.Queries;

public class GetOrganismDetailQuery : IRequest<OrganismVm>
{
    public int Id { get; set; }

    public GetOrganismDetailQuery(int id)
    {
        Id = id;
    }

    internal class GetOrganismHandler : IRequestHandler<GetOrganismDetailQuery, OrganismVm>
    {
        private readonly IRepositoryBase<Organism> _repo;
        private readonly IMapper _mapper;

        public GetOrganismHandler(IRepositoryBase<Organism> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OrganismVm> Handle(GetOrganismDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException($"Could not locate Record with id: {request.Id}");
            return _mapper.Map<OrganismVm>(entity);
        }
    }
}