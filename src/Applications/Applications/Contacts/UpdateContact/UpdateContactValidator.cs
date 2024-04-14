using static Application.Common.Validators;

namespace Application.Contacts.UpdateContact;

public class UpdateContactValidator : AbstractValidator<UpdateContactRequest>
{
    public UpdateContactValidator()
    {
        RuleFor(v => v.Value)
           .NotEmpty();

        RuleFor(contact => contact.Value)
            .Must(IsPhone)
            .WithMessage("The provided value must be a valid phone.")
            .When(customer => customer.Type == Domain.Entities.TypeEnum.Phone);

        RuleFor(contact => contact.Value)
            .Must(IsEmail)
            .WithMessage("The provided value must be a valid email.")
            .When(customer => customer.Type == Domain.Entities.TypeEnum.Email);

        RuleFor(contact => contact.Value)
            .Must(IsWeb)
            .WithMessage("The provided value must be a valid email.")
            .When(customer => customer.Type == Domain.Entities.TypeEnum.Web);
    }
}
