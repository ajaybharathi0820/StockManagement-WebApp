using System.Security.Cryptography;
using System.Text;

namespace Identity.Domain.ValueObjects
{
    // public record Password
    // {
    //     public string Hash { get; }

    //     private Password() { }

    //     public Password(string plainTextPassword)
    //     {
    //         if (string.IsNullOrWhiteSpace(plainTextPassword))
    //             throw new ArgumentException("Password cannot be empty");

    //         Hash = HashPassword(plainTextPassword);
    //     }

    //     private static string HashPassword(string password)
    //     {
    //         using var sha256 = SHA256.Create();
    //         var bytes = Encoding.UTF8.GetBytes(password);
    //         var hash = sha256.ComputeHash(bytes);
    //         return Convert.ToBase64String(hash);
    //     }

    //     public bool Verify(string plainTextPassword)
    //     {
    //         return Hash == HashPassword(plainTextPassword);
    //     }

    //     public override string ToString() => Hash;
    // }
}
