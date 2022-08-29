using Ardalis.Specification;
using FluentValidation;
using UniquomeApp.Domain;
using UniquomeApp.SharedKernel;

namespace UniquomeApp.Application.Organisms.Commands;
//
// public class UpdateOrganismCommandValidator : AbstractValidator<UpdateOrganismCommand>
// {
//     private readonly IRepositoryBase<Organism> _repo;
//
//     public UpdateOrganismCommandValidator(IRepositoryBase<Organism> repo)
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
//         var spec = new OrganismsSpec(id, name);
//         return await _repo.CountAsync(spec, cancellationToken) == 0;
//     }
//
//     internal sealed class OrganismsSpec : Specification<Organism>
//     {
//         public OrganismsSpec(int id, string name)
//         {
//             Query.Where(x => (x.Name == name) && x.Id != id);
//         }
//     }
// }