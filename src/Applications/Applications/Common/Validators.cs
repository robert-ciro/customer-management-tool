
using System.Text.RegularExpressions;

namespace Application.Common;

public static partial class Validators
{
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