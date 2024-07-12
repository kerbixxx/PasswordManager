namespace BackEnd.DTOs
{
    public class CredentialDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsPasswordVisible { get; set; } = false;
    }
}
