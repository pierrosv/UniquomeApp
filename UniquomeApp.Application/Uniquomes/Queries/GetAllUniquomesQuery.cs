using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Uniquomes.Queries;

public class GetAllUniquomesQuery : IRequest<IList<UniquomeVm>>
{
    internal class GetAllUniquomesHandler : IRequestHandler<GetAllUniquomesQuery, IList<UniquomeVm>>
    {
        private readonly IRepositoryBase<Uniquome> _repo;
        private readonly IMapper _mapper;

        public GetAllUniquomesHandler(IRepositoryBase<Uniquome> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IList<UniquomeVm>> Handle(GetAllUniquomesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repo.ListAsync(cancellationToken);
            return _mapper.Map<List<Uniquome>, List<UniquomeVm>>(entities.ToList());
        }
    }
}