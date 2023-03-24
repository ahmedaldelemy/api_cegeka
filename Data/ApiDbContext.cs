using api_cegeka.Models;
using Microsoft.EntityFrameworkCore;

namespace api_cegeka.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
} 