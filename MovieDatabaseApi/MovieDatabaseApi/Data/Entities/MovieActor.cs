namespace MovieDatabaseApi.Data.Entities
{
    public class MovieActor
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public Actor Actor { get; set; }
        public int ActorId { get; set; }

    }
}
