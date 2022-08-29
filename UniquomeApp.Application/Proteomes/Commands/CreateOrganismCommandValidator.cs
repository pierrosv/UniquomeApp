namespace UniquomeApp.Application.Proteomes.Commands;

    // public class CreateProteomeCommandValidator : AbstractValidator<CreateProteomeCommand>
    // {
    //     private readonly IRepositoryBase<Proteome> _repo;
    //
    //
    //     public CreateProteomeCommandValidator(IRepositoryBase<Proteome> repo)
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
    //         var spec = new ProteomeByNameSpec(name);
    //         return await _repo.CountAsync(spec, cancellationToken) == 0;
    //     }
    // }