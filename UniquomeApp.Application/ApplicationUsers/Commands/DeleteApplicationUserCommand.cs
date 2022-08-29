using Ardalis.Specification;
using MediatR;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.ApplicationUsers.Commands;

public class DeleteApplicationUserCommand: IRequest
{
    //TODO: Add Validator for deletion
    public int Id { get; set; }

    public DeleteApplicationUserCommand(int id)
    {
        Id = id;
    }

    internal class DeleteApplicationUserHandler : IRequestHandler<DeleteApplicationUserCommand>
    {
        private readonly IRepositoryBase<ApplicationUser> _repo;

        public DeleteApplicationUserHandler(IRepositoryBase<ApplicationUser> repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                return Unit.Value;
            await _repo.DeleteAsync(entity, cancellationToken);
            return Unit.Value;
        }
    }
}