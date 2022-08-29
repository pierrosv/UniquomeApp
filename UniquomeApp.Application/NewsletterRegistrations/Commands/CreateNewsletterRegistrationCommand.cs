using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Application.Mappings;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.NewsletterRegistrations.Commands;

public class CreateNewsletterRegistrationCommand : IRequest<long>, IMapTo<NewsletterRegistration>
{
    public string Email { get; set; } = default!;
    internal class CreateNewsletterRegistrationHandler : IRequestHandler<CreateNewsletterRegistrationCommand, long>
    {
        private readonly IRepositoryBase<NewsletterRegistration> _repo;
        private readonly IMapper _mapper;
            
        public CreateNewsletterRegistrationHandler(IRepositoryBase<NewsletterRegistration> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateNewsletterRegistrationCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<NewsletterRegistration>(request);
            await _repo.AddAsync(entity, cancellationToken);
            return entity.Id;
        }
    }
}