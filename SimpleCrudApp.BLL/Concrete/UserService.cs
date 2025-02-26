using SimpleCrudApp.BLL.Abstract;
using SimpleCrudApp.DAL.Abstract;
using SimpleCrudApp.Models.DTO;
using SimpleCrudApp.Models.Entities;

namespace SimpleCrudApp.BLL.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetAsync(string id)
        {
            return await _userRepository.GetAsync(id);
        }
        public async Task<string> SaveAsync(User user)
        {
            return await _userRepository.SaveAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            return await _userRepository.Login(loginDto);
        }

        public async Task<bool> Register(RegisterDTO registerDto)
        {
            return await _userRepository.Register(registerDto);
        }

        
    }
}
