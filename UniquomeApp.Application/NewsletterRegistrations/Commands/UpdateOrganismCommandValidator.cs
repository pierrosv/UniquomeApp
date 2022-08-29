namespace UniquomeApp.Application.NewsletterRegistrations.Commands;
//
// public class UpdateNewsletterRegistrationCommandValidator : AbstractValidator<UpdateNewsletterRegistrationCommand>
// {
//     private readonly IRepositoryBase<NewsletterRegistration> _repo;
//
//     public UpdateNewsletterRegistrationCommandValidator(IRepositoryBase<NewsletterRegistration> repo)
//     {
//         _repo = repo;
//
//         RuleFor(x => x.Name)
//             .NotEmpty().WithMessage(CoreMessages.RequiredField)
//             .MaximumLength(Sizes.ParameterName).WithMessage(CoreMessages.ExceededMaxSize);
//
//         RuleFor(x => new {x.Id, x.Name})
//             .MustAsync(async (command, values, cancellation) =>
//             {
//                 var exists = await NameIsUnique(values.Id, values.Name, cancellation);
//                 return exists;
//             }).WithMessage(CoreMessages.ValueAlreadyExists);
//     }
//
//     public async Task<bool> NameIsUnique(int id, string name, CancellationToken cancellationToken)
//     {
//         var spec = new NewsletterRegistrationsSpec(id, name);
//         return await _repo.CountAsync(spec, cancellationToken) == 0;
//     }
//
//     internal sealed class NewsletterRegistrationsSpec : Specification<NewsletterRegistration>
//     {
//         public NewsletterRegistrationsSpec(int id, string name)
//         {
//             Query.Where(x => (x.Name == name) && x.Id != id);
//         }
//     }
// }