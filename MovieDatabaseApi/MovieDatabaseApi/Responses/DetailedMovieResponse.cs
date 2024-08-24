using Microsoft.AspNetCore.Mvc.ModelBinding;
using MovieDatabaseApi.Data.Entities;

namespace MovieDatabaseApi.Responses
{
    public class DetailedMovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<int> ActorIds { get; set; } = new List<int>();
        public Actor? Actor { get; set; }
        public List<int> DirectorIds { get; set; } = new List<int>();
        public Director? Director { get; set; }
        public List<int> ProducerIds { get; set; } = new List<int>();
        Producer? Producer { get; set; }
    }
}
