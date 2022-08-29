using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Application.Mappings;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Organisms.Commands;

public class CreateOrganismCommand : IRequest<long>, IMapTo<Organism>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    internal class CreateOrganismHandler : IRequestHandler<CreateOrganismCommand, long>
    {
        private readonly IRepositoryBase<Organism> _repo;
        private readonly IMapper _mapper;
            
        public CreateOrganismHandler(IRepositoryBase<Organism> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateOrganismCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Organism>(request);
            await _repo.AddAsync(entity, cancellationToken);
            return entity.Id;
        }
    }
}