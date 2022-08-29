using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Application.Common.Exceptions;
using UniquomeApp.Application.Mappings;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.ApplicationUsers.Commands;

public class UpdateApplicationUserCommand: IRequest, IMapTo<ApplicationUser>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    internal class UpdateApplicationUserHandler : IRequestHandler<UpdateApplicationUserCommand>
    {
        private readonly IRepositoryBase<ApplicationUser> _repo;
        private readonly IMapper _mapper;

        public UpdateApplicationUserHandler(IRepositoryBase<ApplicationUser> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
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