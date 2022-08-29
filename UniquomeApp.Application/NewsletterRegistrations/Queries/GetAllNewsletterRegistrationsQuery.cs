using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.NewsletterRegistrations.Queries;

public class GetAllNewsletterRegistrationsQuery : IRequest<IList<NewsletterRegistrationVm>>
{
    internal class GetAllNewsletterRegistrationsHandler : IRequestHandler<GetAllNewsletterRegistrationsQuery, IList<NewsletterRegistrationVm>>
    {
        private readonly IRepositoryBase<NewsletterRegistration> _repo;
        private readonly IMapper _mapper;

        public GetAllNewsletterRegistrationsHandler(IRepositoryBase<NewsletterRegistration> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IList<NewsletterRegistrationVm>> Handle(GetAllNewsletterRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repo.ListAsync(cancellationToken);
            return _mapper.Map<List<NewsletterRegistration>, List<NewsletterRegistrationVm>>(entities.ToList());
        }
    }
}