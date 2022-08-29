using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.ApplicationUsers.Queries;

public class GetAllApplicationUsersQuery : IRequest<IList<ApplicationUserVm>>
{
    internal class GetAllApplicationUsersHandler : IRequestHandler<GetAllApplicationUsersQuery, IList<ApplicationUserVm>>
    {
        private readonly IRepositoryBase<ApplicationUser> _repo;
        private readonly IMapper _mapper;

        public GetAllApplicationUsersHandler(IRepositoryBase<ApplicationUser> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IList<ApplicationUserVm>> Handle(GetAllApplicationUsersQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repo.ListAsync(cancellationToken);
            return _mapper.Map<List<ApplicationUser>, List<ApplicationUserVm>>(entities.ToList());
        }
    }
}