using FluentValidation;
using UniquomeApp.SharedKernel;

namespace UniquomeApp.Application.Organisms.Queries;

public class GetOrganismByIdValidator : AbstractValidator<GetOrganismDetailQuery>
{
    public GetOrganismByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(CoreMessages.RequiredField)
            .GreaterThanOrEqualTo(1).WithMessage(CoreMessages.IncorrectIdValue);
    }
}