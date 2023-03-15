using Microsoft.EntityFrameworkCore;

namespace Data;
public class DataContext :DbContext
{
    public DataContext(DbContextOptions<DataContext> Options) :base(Options)
    {
    }
public DbSet<Student> Students { get; set; }

}