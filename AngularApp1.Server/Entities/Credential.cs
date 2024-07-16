using BackEnd.DTOs;

namespace BackEnd.Entities
{
    public class Credential
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
        public Credential() { }
        public Credential(string name, string password)
        {
            Name = name;
            Password = password;
            CreationTime = DateTime.Now;
        }
        public Credential(UpdateCredentialDto updateDto)
        {
            Id = updateDto.Id;
            Name = updateDto.Name;
            Password = updateDto.Password;
        }

    }
}
