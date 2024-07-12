using BackEnd.Enums;

namespace BackEnd.DTOs
{
    public class UpdateCredentialDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public CredentialType Type { get; set; }
    }
}
