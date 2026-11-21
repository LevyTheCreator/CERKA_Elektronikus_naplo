using System;
using BCrypt.Net;

public static class PasswordEncryption
{
    // Work factor: each +1 doubles the computation time. 12 is a good modern default.
    private const int WorkFactor = 12;

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: WorkFactor);
    }

    public static bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    // Useful when you raise the work factor over time
    public static bool NeedsRehash(string hash)
    {
        return BCrypt.Net.BCrypt.PasswordNeedsRehash(hash, WorkFactor);
    }
}
