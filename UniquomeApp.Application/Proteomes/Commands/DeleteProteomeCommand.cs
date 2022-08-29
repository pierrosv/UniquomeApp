using Ardalis.Specification;
using MediatR;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Proteomes.Commands;

public class DeleteProteomeCommand: IRequest
{
    //TODO: Add Validator for deletion
    public int Id { get; set; }

    public DeleteProteomeCommand(int id)
    {
        Id = id;
    }

    internal class DeleteProteomeHandler : IRequestHandler<DeleteProteomeCommand>
    {
        private readonly IRepositoryBase<Proteome> _repo;

        public DeleteProteomeHandler(IRepositoryBase<Proteome> repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteProteomeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                return Unit.Value;
            await _repo.DeleteAsync(entity, cancellationToken);
            return Unit.Value;
        }
    }
}