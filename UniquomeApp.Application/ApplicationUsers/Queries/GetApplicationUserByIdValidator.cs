using FluentValidation;
using UniquomeApp.SharedKernel;

namespace UniquomeApp.Application.ApplicationUsers.Queries;

public class GetApplicationUserByIdValidator : AbstractValidator<GetApplicationUserDetailQuery>
{
    public GetApplicationUserByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(CoreMessages.RequiredField)
            .GreaterThanOrEqualTo(1).WithMessage(CoreMessages.IncorrectIdValue);
    }
}