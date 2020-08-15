namespace PasswordManagerV3.Other.Security
{
    public interface ICipher
    {
        string Decrypt(string input);
        string Encrypt(string input);
    }
}