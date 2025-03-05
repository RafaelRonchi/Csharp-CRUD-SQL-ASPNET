using BlazorApp.Model;
using System.Net.Http.Json;

namespace BlazorApp.Service
{
    public class UserService
    {
        HttpClient httpClient = new HttpClient();

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> CreateUser(User user)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("https://localhost:44393/CreateUser", user);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateUser: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync("https://localhost:44393/UpdateUser", user);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateUser: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"https://localhost:44393/DeleteUser?id={id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteUser: {ex.Message}");
                return false;
            }
        }

        public async Task<List<User>> SearchUsersAsync(User filters)
        {
            try
            {
                var queryString = string.Empty;

                if (filters.Id != null && filters.Id != 0)
                    queryString += $"&id={filters.Id}";
                if (!string.IsNullOrEmpty(filters.Name))
                    queryString += $"&name={filters.Name}";
                if (!string.IsNullOrEmpty(filters.Company))
                    queryString += $"&company={filters.Company}";
                if (!string.IsNullOrEmpty(filters.PersonalPhone))
                    queryString += $"&personalPhone={filters.PersonalPhone}";
                if (!string.IsNullOrEmpty(filters.WorkPhone))
                    queryString += $"&workPhone={filters.WorkPhone}";
                if (filters.Email != null && filters.Email.Any())
                    queryString += $"&email={string.Join(",", filters.Email)}";

                if (!string.IsNullOrEmpty(queryString))
                {
                    queryString = queryString.Substring(1);
                    var result = await httpClient.GetFromJsonAsync<List<User>>($"https://localhost:44393/GetUsersWithFilters?{queryString}");
                    return result ?? new List<User>();
                }
                else
                {
                    var result = await httpClient.GetFromJsonAsync<List<User>>($"https://localhost:44393/GetUsersWithFilters");
                    return result ?? new List<User>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchUsersAsync: {ex.Message}");
                return new List<User>();
            }
        }

    }
}
