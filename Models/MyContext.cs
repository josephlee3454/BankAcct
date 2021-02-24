using bankAccounts.Models;
using Microsoft.EntityFrameworkCore;
namespace bankAccounts.Models
{
    public class MyContext : DbContext

    {
    // base() calls the parent class' constructor passing the "options" parameter along
    public MyContext(DbContextOptions options) : base(options) { }

    // "users" table is represented by this DbSet "Users"
    public DbSet<UserModel> Users { get; set; }
    public DbSet<TransacM> Transactions { get; set;}

    public DbSet<UserLog> UserLogs {get;set;}

    public DbSet<UserRegistration> UserRegistrations {get;set;}

    }
}