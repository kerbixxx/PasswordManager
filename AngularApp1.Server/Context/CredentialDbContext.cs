using BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Context
{
    public class CredentialDbContext : DbContext
    {
        public DbSet<Credential> Credentials { get; set; }
        public CredentialDbContext(DbContextOptions<CredentialDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
