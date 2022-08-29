using FluentValidation;
using UniquomeApp.SharedKernel;

namespace UniquomeApp.Application.Uniquomes.Queries;

public class GetUniquomeByIdValidator : AbstractValidator<GetUniquomeDetailQuery>
{
    public GetUniquomeByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(CoreMessages.RequiredField)
            .GreaterThanOrEqualTo(1).WithMessage(CoreMessages.IncorrectIdValue);
    }
}