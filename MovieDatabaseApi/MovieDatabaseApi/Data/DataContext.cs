using Microsoft.EntityFrameworkCore;
using MovieDatabaseApi.Data.Entities;

namespace MovieDatabaseApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }
    }
}
