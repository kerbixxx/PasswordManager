using BackEnd.Interfaces;
using System.Text.RegularExpressions;

namespace BackEnd.Implementations
{
    public class EmailCredentialStrategy : ICredentialStrategy
    {
        public bool IsValidName(string name)
        {
            var emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
            return emailRegex.IsMatch(name);
        }

        public bool IsValidPassword(string password)
        {
            return password.Length >= 8;
        }
    }
}
