using Ardalis.Specification;
using MediatR;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.ApplicationUsers.Commands;

public class DeleteOrganismCommand: IRequest
{
    //TODO: Add Validator for deletion
    public int Id { get; set; }

    public DeleteOrganismCommand(int id)
    {
        Id = id;
    }

    internal class DeleteOrganismHandler : IRequestHandler<DeleteOrganismCommand>
    {
        private readonly IRepositoryBase<Organism> _repo;

        public DeleteOrganismHandler(IRepositoryBase<Organism> repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteOrganismCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                return Unit.Value;
            await _repo.DeleteAsync(entity, cancellationToken);
            return Unit.Value;
        }
    }
}