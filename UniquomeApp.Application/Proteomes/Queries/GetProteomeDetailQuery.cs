using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Application.Common.Exceptions;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Proteomes.Queries;

public class GetProteomeDetailQuery : IRequest<ProteomeVm>
{
    public int Id { get; set; }

    public GetProteomeDetailQuery(int id)
    {
        Id = id;
    }

    internal class GetProteomeHandler : IRequestHandler<GetProteomeDetailQuery, ProteomeVm>
    {
        private readonly IRepositoryBase<Proteome> _repo;
        private readonly IMapper _mapper;

        public GetProteomeHandler(IRepositoryBase<Proteome> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProteomeVm> Handle(GetProteomeDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException($"Could not locate Record with id: {request.Id}");
            return _mapper.Map<ProteomeVm>(entity);
        }
    }
}