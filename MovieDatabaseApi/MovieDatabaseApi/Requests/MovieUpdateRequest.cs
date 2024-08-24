namespace MovieDatabaseApi.Requests
{
    public class MovieUpdateRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<int> ActorIds { get; set; } = new List<int>();
        public List<int> DirectorIds { get; set; } = new List<int>();
        public List<int> ProducerIds { get; set; } = new List<int>();
    }
}
