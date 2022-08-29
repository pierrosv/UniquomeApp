using Ardalis.Specification;
using MediatR;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Uniquomes.Commands;

public class DeleteUniquomeCommand: IRequest
{
    //TODO: Add Validator for deletion
    public int Id { get; set; }

    public DeleteUniquomeCommand(int id)
    {
        Id = id;
    }

    internal class DeleteUniquomeHandler : IRequestHandler<DeleteUniquomeCommand>
    {
        private readonly IRepositoryBase<Uniquome> _repo;

        public DeleteUniquomeHandler(IRepositoryBase<Uniquome> repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteUniquomeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                return Unit.Value;
            await _repo.DeleteAsync(entity, cancellationToken);
            return Unit.Value;
        }
    }
}