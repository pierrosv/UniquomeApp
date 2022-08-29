using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Application.Common.Exceptions;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Uniquomes.Queries;

public class GetUniquomeDetailQuery : IRequest<UniquomeVm>
{
    public int Id { get; set; }

    public GetUniquomeDetailQuery(int id)
    {
        Id = id;
    }

    internal class GetUniquomeHandler : IRequestHandler<GetUniquomeDetailQuery, UniquomeVm>
    {
        private readonly IRepositoryBase<Uniquome> _repo;
        private readonly IMapper _mapper;

        public GetUniquomeHandler(IRepositoryBase<Uniquome> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UniquomeVm> Handle(GetUniquomeDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException($"Could not locate Record with id: {request.Id}");
            return _mapper.Map<UniquomeVm>(entity);
        }
    }
}