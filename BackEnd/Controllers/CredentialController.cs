using BackEnd.Context;
using BackEnd.DTOs;
using BackEnd.Entities;
using BackEnd.Enums;
using BackEnd.Implementations;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialController : ControllerBase
    {
        private readonly CredentialDbContext _db;
        public CredentialController(CredentialDbContext db)
        {
            _db = db;

            Seed();
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] string? name)
        {
            IQueryable<Credential> query = _db.Credentials.AsQueryable();

            if (string.IsNullOrEmpty(name))
            {
                var credentials = await query.ToListAsync();
                var vmCredentials = credentials.Select(c => new CredentialDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Password = c.Password,
                    CreationTime = c.CreationTime,
                    IsPasswordVisible = false
                }).ToList();
                return Ok(vmCredentials);
            }

            var filteredCredentials = await query.Where(c => c.Name.Contains(name)).OrderBy(x => x.CreationTime).ToListAsync();
            var vmFilteredCredentials = filteredCredentials.Select(c => new CredentialDto
            {
                Id = c.Id,
                Name = c.Name,
                Password = c.Password,
                CreationTime = c.CreationTime,
                IsPasswordVisible = false
            }).ToList();
            return Ok(vmFilteredCredentials);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var obj = await _db.Credentials.FindAsync(id);
            if (obj == null) return NotFound("Not found");
            return Ok(obj);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCredentialDto dto)
        {
            ICredentialStrategy strategy = CreateStrategy(dto.Type);

            if (strategy == null) return BadRequest("Invalid Type");

            if(!strategy.IsValidName(dto.Name))
            {
                return BadRequest("Invalid Name");
            }

            if (!strategy.IsValidPassword(dto.Password))
            {
                return BadRequest("Invalid Password");
            }

            if(_db.Credentials.Where(c=>c.Name == dto.Name).Any())
            {
                return BadRequest("Name already exists");
            }

            await _db.Credentials.AddAsync(new Credential(dto.Name, dto.Password));
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateCredentialDto dto)
        {
            ICredentialStrategy strategy = CreateStrategy(dto.Type);

            if (strategy == null) return BadRequest("Invalid Type");

            if (!strategy.IsValidName(dto.Name))
            {
                return BadRequest("Invalid Name");
            }

            if (!strategy.IsValidPassword(dto.Password))
            {
                return BadRequest("Invalid Password");
            }
            Credential obj = new(dto);
            _db.Credentials.Update(obj);
            await _db.SaveChangesAsync();
            return Ok(obj);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var obj = await _db.Credentials.FindAsync(id);
            if (obj == null) return NotFound();
            _db.Credentials.Remove(obj);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [NonAction]
        void Seed()
        {
            if (!_db.Credentials.Any())
            {
                _db.Credentials.Add(new Credential("test@test.com", "12345678"));
                _db.Credentials.Add(new Credential("admin@admin.com", "45332432"));
                _db.Credentials.Add(new Credential("JohnDoe@test.com", "TestPassword"));
                _db.Credentials.Add(new Credential("google.com", "TestPassword"));
                _db.Credentials.Add(new Credential("facebook.com", "4324234234"));
                _db.Credentials.Add(new Credential("vk.com", "9848236478"));
            }
            _db.SaveChanges();
        }

        [NonAction]
        ICredentialStrategy CreateStrategy(CredentialType type)
        {
            switch (type)
            {
                case CredentialType.Email:
                    return new EmailCredentialStrategy();
                case CredentialType.Site:
                    return new SiteCredentialStrategy();
                default:
                    return null;
            }
        }
    }
}
