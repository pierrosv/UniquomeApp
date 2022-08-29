namespace UniquomeApp.Application.NewsletterRegistrations.Commands;

    // public class CreateNewsletterRegistrationCommandValidator : AbstractValidator<CreateNewsletterRegistrationCommand>
    // {
    //     private readonly IRepositoryBase<NewsletterRegistration> _repo;
    //
    //
    //     public CreateNewsletterRegistrationCommandValidator(IRepositoryBase<NewsletterRegistration> repo)
    //     {
    //         _repo = repo;
    //
    //         RuleFor(x => x.Name)
    //             .NotEmpty().WithMessage(CoreMessages.RequiredField)
    //             .MaximumLength(Sizes.ParameterName).WithMessage(CoreMessages.ExceededMaxSize)
    //             .MustAsync(NameIsUnique).WithMessage(CoreMessages.ValueAlreadyExists);
    //     }
    //
    //     public async Task<bool> NameIsUnique(string name, CancellationToken cancellationToken)
    //     {
    //         var spec = new NewsletterRegistrationByNameSpec(name);
    //         return await _repo.CountAsync(spec, cancellationToken) == 0;
    //     }
    // }