using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Proteomes.Queries;

public class GetAllProteomesQuery : IRequest<IList<ProteomeVm>>
{
    internal class GetAllProteomesHandler : IRequestHandler<GetAllProteomesQuery, IList<ProteomeVm>>
    {
        private readonly IRepositoryBase<Proteome> _repo;
        private readonly IMapper _mapper;

        public GetAllProteomesHandler(IRepositoryBase<Proteome> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IList<ProteomeVm>> Handle(GetAllProteomesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repo.ListAsync(cancellationToken);
            return _mapper.Map<List<Proteome>, List<ProteomeVm>>(entities.ToList());
        }
    }
}