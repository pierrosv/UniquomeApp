using FluentValidation;
using UniquomeApp.SharedKernel;

namespace UniquomeApp.Application.NewsletterRegistrations.Queries;

public class GetNewsletterRegistrationByIdValidator : AbstractValidator<GetNewsletterRegistrationDetailQuery>
{
    public GetNewsletterRegistrationByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(CoreMessages.RequiredField)
            .GreaterThanOrEqualTo(1).WithMessage(CoreMessages.IncorrectIdValue);
    }
}