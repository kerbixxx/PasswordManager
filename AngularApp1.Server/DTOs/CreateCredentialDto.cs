using BackEnd.Enums;

namespace BackEnd.DTOs
{
    public class CreateCredentialDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public CredentialType Type { get; set; }
    }
}
