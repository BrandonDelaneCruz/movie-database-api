using Microsoft.EntityFrameworkCore;
using MovieDatabaseApi.Data.Entities;

namespace MovieDatabaseApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<MovieActor> MoviesActors { get; set; }
        public DbSet<MovieDirector> MovieDirectors { get; set; }
        public DbSet<MovieProducer> MovieProducers { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }
    }
}
