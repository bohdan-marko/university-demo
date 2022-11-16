using Microsoft.EntityFrameworkCore;
using University.DAL.Domain;

namespace University.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<Job> Job { get; set; }
    public DbSet<Worker> Worker { get; set; }
    public DbSet<Workplace> Workplace { get; set; }
    public DbSet<User> User { get; set; }
}