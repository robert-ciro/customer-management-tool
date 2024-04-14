using System.Text.RegularExpressions;

namespace Application.Contacts.CreateContact;

public partial class CreateContactValidator : AbstractValidator<CreateContactRequest>
{
    public CreateContactValidator()
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

    [GeneratedRegex(@"^\+(?:[0-9] ?){6,14}[0-9]$")]
    private static partial Regex IsPhoneRegex();

    public static bool IsPhone(string value) => IsPhoneRegex().IsMatch(value);

    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    private static partial Regex IsEmailRegex();

    public static bool IsEmail(string value) => IsEmailRegex().IsMatch(value);

    [GeneratedRegex(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$")]
    private static partial Regex IsWebRegex();

    public static bool IsWeb(string value) => IsWebRegex().IsMatch(value);

}
