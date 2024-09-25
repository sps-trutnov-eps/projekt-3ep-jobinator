using Microsoft.EntityFrameworkCore;
using Jobinator.Models;

namespace Jobinator.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
}
