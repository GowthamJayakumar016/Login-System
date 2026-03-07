using CreditCardAPI.Data;
using CreditCardAPI.Models;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    
    public async Task<Users> GetUserByUsername(string username)
    {
        return await _context.Users
        .FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<Users> GetUserByEmail(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task CreateUser(Users user)
    {
        await _context.Users.AddAsync(user);
    }

   
    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}