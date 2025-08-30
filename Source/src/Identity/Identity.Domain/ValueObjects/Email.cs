using System.Text.RegularExpressions;

namespace Identity.Domain.ValueObjects
{
    // public record Email
    // {
    //     public string Value { get; }

    //     private Email() { }

    //     public Email(string value)
    //     {
    //         if (string.IsNullOrWhiteSpace(value))
    //             throw new ArgumentException("Email cannot be empty");

    //         if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
    //             throw new ArgumentException("Invalid email format");

    //         Value = value;
    //     }

    //     public override string ToString() => Value;
    // }
}
