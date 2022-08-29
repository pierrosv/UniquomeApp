using Ardalis.Specification;
using MediatR;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.NewsletterRegistrations.Commands;

public class DeleteNewsletterRegistrationCommand: IRequest
{
    //TODO: Add Validator for deletion
    public int Id { get; set; }

    public DeleteNewsletterRegistrationCommand(int id)
    {
        Id = id;
    }

    internal class DeleteNewsletterRegistrationHandler : IRequestHandler<DeleteNewsletterRegistrationCommand>
    {
        private readonly IRepositoryBase<NewsletterRegistration> _repo;

        public DeleteNewsletterRegistrationHandler(IRepositoryBase<NewsletterRegistration> repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteNewsletterRegistrationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                return Unit.Value;
            await _repo.DeleteAsync(entity, cancellationToken);
            return Unit.Value;
        }
    }
}