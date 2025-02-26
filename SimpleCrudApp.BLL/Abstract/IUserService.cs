
using SimpleCrudApp.Models.DTO;
using SimpleCrudApp.Models.Entities;

namespace SimpleCrudApp.BLL.Abstract
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetAsync(string id);
        Task<string> SaveAsync(User user);
        Task DeleteAsync(string id);
        Task<AuthResponseDto> Login(LoginDto loginDto);
        Task<bool> Register(RegisterDTO registerDto);
    }
}
