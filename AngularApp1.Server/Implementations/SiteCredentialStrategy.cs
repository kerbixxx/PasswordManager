using BackEnd.Interfaces;

namespace BackEnd.Implementations
{
    public class SiteCredentialStrategy : ICredentialStrategy
    {
        public bool IsValidName(string name)
        {
            return true;
        }

        public bool IsValidPassword(string password)
        {
            return password.Length >= 8;
        }
    }
}
