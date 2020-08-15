namespace PasswordManagerV3.Other.Security
{
    internal class TestCipher : ICipher
    {
        public string Decrypt(string input)
        {
            return input;
        }

        public string Encrypt(string input)
        {
            return input;
        }
    }
}