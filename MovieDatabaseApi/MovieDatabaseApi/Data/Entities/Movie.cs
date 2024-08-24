namespace MovieDatabaseApi.Data.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
        public List<MovieDirector> MovieDirectors { get; set; } = new List<MovieDirector>();
        public List<MovieProducer> MovieProducers { get; set; } = new List<MovieProducer>();
    }
}
