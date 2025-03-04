using Microsoft.EntityFrameworkCore;
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

    public async Task<List<User>> SearchUsersAsync(User filters)
    {
        var query = dataContext.Users.AsQueryable();

        if (filters.Id != 0)
        {
            query = query.Where(user => user.Id == filters.Id);
        }

        if (!string.IsNullOrEmpty(filters.Name))
        {
            query = query.Where(user => user.Name.Contains(filters.Name));
        }

        if (!string.IsNullOrEmpty(filters.Company))
        {
            query = query.Where(user => user.Company != null && user.Company.Contains(filters.Company));
        }

        if (filters.Email != null && filters.Email.Count > 0)
        {
            query = query.Where(user => user.Email != null && user.Email.Any(e => filters.Email.Contains(e)));            
        }

        if (!string.IsNullOrEmpty(filters.PersonalPhone))
        {
            query = query.Where(user => user.PersonalPhone != null && user.PersonalPhone.Contains(filters.PersonalPhone));
        }
        
        if (!string.IsNullOrEmpty(filters.WorkPhone))
        {
            query = query.Where(user => user.WorkPhone != null && user.WorkPhone.Contains(filters.WorkPhone));
        }
        
        return await query.ToListAsync();
    }

    private void UpdateUserVariables(User userToUpdate, User userUpdate)
    {
        userToUpdate.Name = userUpdate.Name;
        userToUpdate.Email = userUpdate.Email;
        userToUpdate.Company = userUpdate.Company;
    }
}