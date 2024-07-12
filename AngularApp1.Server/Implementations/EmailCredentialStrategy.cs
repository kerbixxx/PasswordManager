using BackEnd.Interfaces;
using System.Text.RegularExpressions;

namespace BackEnd.Implementations
{
    public class EmailCredentialStrategy : ICredentialStrategy
    {
        public bool IsValidName(string name)
        {
            var emailRegex = new Regex(@"^((([0-9A-Za-z]{1}[-0-9A-z\.]{0,30}[0-9A-Za-z]?)|([0-9А-Яа-я]{1}[-0-9А-я\.]{0,30}[0-9А-Яа-я]?))@([-A-Za-z]{1,}\.){1,}[-A-Za-z]{2,})$");
            return emailRegex.IsMatch(name);
        }

        public bool IsValidPassword(string password)
        {
            return password.Length >= 8;
        }
    }
}
