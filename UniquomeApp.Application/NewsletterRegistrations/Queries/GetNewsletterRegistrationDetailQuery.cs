using Ardalis.Specification;
using AutoMapper;
using MediatR;
using UniquomeApp.Application.Common.Exceptions;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.NewsletterRegistrations.Queries;

public class GetNewsletterRegistrationDetailQuery : IRequest<NewsletterRegistrationVm>
{
    public int Id { get; set; }

    public GetNewsletterRegistrationDetailQuery(int id)
    {
        Id = id;
    }

    internal class GetNewsletterRegistrationHandler : IRequestHandler<GetNewsletterRegistrationDetailQuery, NewsletterRegistrationVm>
    {
        private readonly IRepositoryBase<NewsletterRegistration> _repo;
        private readonly IMapper _mapper;

        public GetNewsletterRegistrationHandler(IRepositoryBase<NewsletterRegistration> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<NewsletterRegistrationVm> Handle(GetNewsletterRegistrationDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException($"Could not locate Record with id: {request.Id}");
            return _mapper.Map<NewsletterRegistrationVm>(entity);
        }
    }
}