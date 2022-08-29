namespace UniquomeApp.Application.ApplicationUsers.Commands;

    // public class CreateOrganismCommandValidator : AbstractValidator<CreateOrganismCommand>
    // {
    //     private readonly IRepositoryBase<Organism> _repo;
    //
    //
    //     public CreateOrganismCommandValidator(IRepositoryBase<Organism> repo)
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
    //         var spec = new OrganismByNameSpec(name);
    //         return await _repo.CountAsync(spec, cancellationToken) == 0;
    //     }
    // }