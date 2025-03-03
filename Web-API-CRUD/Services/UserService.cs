using Web_API_CRUD.Data;
using Web_API_CRUD.Models;
using Web_API_CRUD.Services.IServices;

namespace Web_API_CRUD.Services;

public class UserService(DataContext dataContext) : IUserService
{
    public async Task<bool> CreateUser(User user)
    {
        try
        {
            await dataContext.Users.AddAsync(user);
            await dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> DeleteUser(int id)
    {
        try
        {
            var user = await GetUserByIdAsync(id);
            if (user == null) return false;

            dataContext.Users.Remove(user);
            await dataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> UpdateUser(User user)
    {
        try
        {
            var userToUpdate = await GetUserByIdAsync(user.Id);
            if (userToUpdate == null) return false;

            UpdateUserVariables(userToUpdate, user);
            await dataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await dataContext.Users
            .FindAsync(id);
    }

    private void UpdateUserVariables(User userToUpdate, User userUpdate)
    {
        userToUpdate.Name = userUpdate.Name;
        userToUpdate.Email = userUpdate.Email;
        userToUpdate.Company = userUpdate.Company;
    }
}