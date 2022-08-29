namespace UniquomeApp.Application.Uniquomes.Commands;

    // public class CreateUniquomeCommandValidator : AbstractValidator<CreateUniquomeCommand>
    // {
    //     private readonly IRepositoryBase<Uniquome> _repo;
    //
    //
    //     public CreateUniquomeCommandValidator(IRepositoryBase<Uniquome> repo)
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
    //         var spec = new UniquomeByNameSpec(name);
    //         return await _repo.CountAsync(spec, cancellationToken) == 0;
    //     }
    // }