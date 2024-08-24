using Microsoft.Identity.Client;

namespace MovieDatabaseApi.Data.Entities
{
    public class MovieProducer
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public int MovieId {  get; set; }
        public Producer Producer { get; set; }
        public int ProducerId { get; set; }
    }
}
