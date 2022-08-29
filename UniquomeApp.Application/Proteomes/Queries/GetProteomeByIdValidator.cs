using FluentValidation;
using UniquomeApp.SharedKernel;

namespace UniquomeApp.Application.Proteomes.Queries;

public class GetProteomeByIdValidator : AbstractValidator<GetProteomeDetailQuery>
{
    public GetProteomeByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(CoreMessages.RequiredField)
            .GreaterThanOrEqualTo(1).WithMessage(CoreMessages.IncorrectIdValue);
    }
}