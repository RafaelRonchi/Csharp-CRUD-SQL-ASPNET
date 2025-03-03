using Web_API_CRUD.Models;

namespace Web_API_CRUD.Services.IServices
{
    public interface IUserService
    {
        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<User?> GetUserByIdAsync(int id);
    }
}
