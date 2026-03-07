using CreditCardAPI.Models;

public interface IUserRepository
{
    Task<Users> GetUserByUsername(string username);

    Task<Users> GetUserByEmail(string email);

    Task CreateUser(Users user);

    Task Save();
}