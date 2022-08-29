namespace UniquomeApp.Application.Proteomes.Commands;
//
// public class UpdateProteomeCommandValidator : AbstractValidator<UpdateProteomeCommand>
// {
//     private readonly IRepositoryBase<Proteome> _repo;
//
//     public UpdateProteomeCommandValidator(IRepositoryBase<Proteome> repo)
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
//         var spec = new ProteomesSpec(id, name);
//         return await _repo.CountAsync(spec, cancellationToken) == 0;
//     }
//
//     internal sealed class ProteomesSpec : Specification<Proteome>
//     {
//         public ProteomesSpec(int id, string name)
//         {
//             Query.Where(x => (x.Name == name) && x.Id != id);
//         }
//     }
// }