using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Application.Common.Exceptions;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.ApplicationUsers.Queries;

public class GetApplicationUserDetailQuery : IRequest<ApplicationUserVm>
{
    public int Id { get; set; }

    public GetApplicationUserDetailQuery(int id)
    {
        Id = id;
    }

    internal class GetApplicationUserHandler : IRequestHandler<GetApplicationUserDetailQuery, ApplicationUserVm>
    {
        private readonly IRepositoryBase<ApplicationUser> _repo;
        private readonly IMapper _mapper;

        public GetApplicationUserHandler(IRepositoryBase<ApplicationUser> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApplicationUserVm> Handle(GetApplicationUserDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException($"Could not locate Record with id: {request.Id}");
            return _mapper.Map<ApplicationUserVm>(entity);
        }
    }
}