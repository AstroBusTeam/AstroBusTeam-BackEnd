using System.Security.Cryptography;

namespace AstroBusTeamBackEnd.Iam.Domain.Services;

/// <summary>
///     Hashes and verifies passwords with PBKDF2 (HMAC-SHA256).
/// </summary>
public static class PasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 100_000;
    private const int MinLength = 8;

    /// <summary>
    ///     Hashes a plain-text password.
    /// </summary>
    /// <param name="password">The plain-text password.</param>
    /// <returns>A string with the algorithm, iterations, salt and hash.</returns>
    /// <exception cref="ArgumentException">Thrown when the password is blank or shorter than 8 characters.</exception>
    public static string Hash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be null, empty, or whitespace.", nameof(password));
        if (password.Length < MinLength)
            throw new ArgumentException($"Password must have at least {MinLength} characters.", nameof(password));

        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var key = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
        return $"pbkdf2-sha256${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(key)}";
    }

    /// <summary>
    ///     Verifies a plain-text password against a stored hash.
    /// </summary>
    /// <param name="password">The plain-text password.</param>
    /// <param name="stored">The stored hash produced by <see cref="Hash"/>.</param>
    /// <returns>True when the password matches.</returns>
    public static bool Verify(string password, string stored)
    {
        var parts = stored.Split('$');
        if (parts.Length != 4 || parts[0] != "pbkdf2-sha256" || !int.TryParse(parts[1], out var iterations)) return false;

        var salt = Convert.FromBase64String(parts[2]);
        var expected = Convert.FromBase64String(parts[3]);
        var actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expected.Length);
        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }
}
