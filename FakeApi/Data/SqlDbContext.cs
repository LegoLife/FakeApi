using FakeApi.Dto;
using Microsoft.EntityFrameworkCore;

namespace FakeApi.Data;

public class SqlDbContext : DbContext
{
    public SqlDbContext(DbContextOptions<SqlDbContext> options):base(options)
    {
        
    }
    
    public DbSet<ComputerRecord> Computer { get; set; }
    public DbSet<VehicleRecord> Vehicle { get; set; }
    
}