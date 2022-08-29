using AutoMapper;
using MediatR;
using UniquomeApp.Application.Specs;
using UniquomeApp.Domain;
using UniquomeApp.Domain.Base;

namespace UniquomeApp.Application.ApplicationUsers.Queries;

public class GetApplicationUserByEmailDetailQuery : IRequest<ApplicationUserVm>
{
    public string Email { get; set; }

    public GetApplicationUserByEmailDetailQuery(string email)
    {
        Email = email;
    }

    internal class GetApplicationUserByEmailHandler : IRequestHandler<GetApplicationUserByEmailDetailQuery, ApplicationUserVm>
    {
        private readonly IUniquomeExtendedRepository<ApplicationUser?> _repo;
        private readonly IMapper _mapper;

        public GetApplicationUserByEmailHandler(
            IUniquomeExtendedRepository<ApplicationUser?> repo, 
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApplicationUserVm> Handle(GetApplicationUserByEmailDetailQuery request, CancellationToken cancellationToken)
        {
            var spec = new ApplicationUserByEmailSpec(request.Email);
            var entity = await _repo.GetAsync(spec, cancellationToken);
            if (entity == null)
                throw new Exception("Δεν υπάρχει ο χρήστης !");
            var vm = _mapper.Map<ApplicationUserVm>(entity);
            return vm;
        }
    }
}