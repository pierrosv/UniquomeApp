using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Application.Common.Exceptions;
using UniquomeApp.Application.Mappings;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Proteomes.Commands;

public class UpdateProteomeCommand: IRequest, IMapTo<Proteome>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Version { get; set; } = default!;
    internal class UpdateProteomeHandler : IRequestHandler<UpdateProteomeCommand>
    {
        private readonly IRepositoryBase<Proteome> _repo;
        private readonly IMapper _mapper;

        public UpdateProteomeHandler(IRepositoryBase<Proteome> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProteomeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException($"Could not locate Record with id: {request.Id}");
            entity = _mapper.Map(request, entity);
            await _repo.UpdateAsync(entity, cancellationToken);
            return Unit.Value;
        }
    }
}