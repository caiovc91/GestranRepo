using System.Security.Cryptography;
using System.Text;

namespace Gestran.Backend.Application.Common.Helpers;

public static class AuthHelper
{
    public static string GenerateHash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return string.Empty;

        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    // Valida senha com hashcode armazenado
    public static bool VerifyHash(string password, string storedHash)
    {
        if (string.IsNullOrEmpty(storedHash)) return false;
        var hash = GenerateHash(password);
        return hash == storedHash;
    }

    // Gerador de token fictício para testes
    public static string GenerateFakeToken(Guid userId, string role)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userId}:{role}:{DateTime.UtcNow.Ticks}"));
    }
}