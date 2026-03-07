using CreditCardAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace CreditCardAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

       
        public DbSet<Users> Users { get; set; }

        


        
    }
}