namespace BackEnd.Interfaces
{
    public interface ICredentialStrategy
    {
        bool IsValidName(string name);
        bool IsValidPassword(string password);
    }
}
