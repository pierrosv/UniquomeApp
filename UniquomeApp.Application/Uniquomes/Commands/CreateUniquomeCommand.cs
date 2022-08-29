using Ardalis.Specification;
using AutoMapper;
using MediatR;
using NodaTime;
using UniquomeApp.Application.Mappings;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Uniquomes.Commands;

public class CreateUniquomeCommand : IRequest<long>, IMapTo<Uniquome>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Version { get; set; } = default!;
    public Instant CreationDate { get; set; }
    internal class CreateUniquomeHandler : IRequestHandler<CreateUniquomeCommand, long>
    {
        private readonly IRepositoryBase<Uniquome> _repo;
        private readonly IMapper _mapper;
            
        public CreateUniquomeHandler(IRepositoryBase<Uniquome> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateUniquomeCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Uniquome>(request);
            await _repo.AddAsync(entity, cancellationToken);
            return entity.Id;
        }
    }
}