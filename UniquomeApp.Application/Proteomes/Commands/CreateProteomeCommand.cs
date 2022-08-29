using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Application.Mappings;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Proteomes.Commands;

public class CreateProteomeCommand : IRequest<long>, IMapTo<Proteome>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Version { get; set; } = default!;
    internal class CreateProteomeHandler : IRequestHandler<CreateProteomeCommand, long>
    {
        private readonly IRepositoryBase<Proteome> _repo;
        private readonly IMapper _mapper;
            
        public CreateProteomeHandler(IRepositoryBase<Proteome> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateProteomeCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Proteome>(request);
            await _repo.AddAsync(entity, cancellationToken);
            return entity.Id;
        }
    }
}