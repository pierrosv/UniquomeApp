using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Application.Mappings;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.ApplicationUsers.Commands;

public class CreateApplicationUserCommand : IRequest<long>, IMapTo<ApplicationUser>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    internal class CreateApplicationUserHandler : IRequestHandler<CreateApplicationUserCommand, long>
    {
        private readonly IRepositoryBase<ApplicationUser> _repo;
        private readonly IMapper _mapper;
            
        public CreateApplicationUserHandler(IRepositoryBase<ApplicationUser> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ApplicationUser>(request);
            await _repo.AddAsync(entity, cancellationToken);
            return entity.Id;
        }
    }
}