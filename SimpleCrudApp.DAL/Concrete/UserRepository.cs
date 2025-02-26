using Microsoft.EntityFrameworkCore;
using SimpleCrudApp.DAL.Abstract;
using SimpleCrudApp.Models.DTO;
using SimpleCrudApp.Models.Entities;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SimpleCrudApp.DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }

        public async Task<string> SaveAsync(User user)
        {
            if (!string.IsNullOrEmpty(user.Id))
            {
                var exists = await _context.Users.AnyAsync(u => u.Id == user.Id);
                if (!exists)
                {
                    throw new KeyNotFoundException($"User with ID {user.Id} not found.");
                }
            }
            _context.Entry(user).State = !string.IsNullOrEmpty(user.Id) ? EntityState.Modified : EntityState.Added;
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _context.Users.FindAsync(id)
                ?? throw new KeyNotFoundException($"User with ID {id} not found.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return null;

            string token = GenerateJwtToken(user);
            return new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1),
                UserId = user.Id
            };
        }


        public async Task<bool> Register(RegisterDTO registerDto)
        {
            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password) // Hash password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }


        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
