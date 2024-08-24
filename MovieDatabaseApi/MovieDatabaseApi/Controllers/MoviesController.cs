using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MovieDatabaseApi.Data;
using MovieDatabaseApi.Data.Entities;
using MovieDatabaseApi.Requests;
using MovieDatabaseApi.Responses;

namespace MovieDatabaseApi.Controllers
{
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DataContext _context;

        public MoviesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("api/movies")]
        public ActionResult<List<MovieResponse>> GetAll()
        {
            List<MovieResponse> movieToReturn = _context.Movies
                .Select(x => new MovieResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                })
                .ToList();

            return Ok(movieToReturn);
        }

        [HttpGet("api/movies/{movieId}")]
        public ActionResult<DetailedMovieResponse> GetById(int movieId)
        {
            Movie? movieFromDatabase = _context.Movies
                .Include(x => x.MovieActors)
                .Include(x => x.MovieDirectors)
                .Include(x => x.MovieProducers)
                .FirstOrDefault(x => x.Id == movieId);

            if (movieFromDatabase == null)
            {
                return NotFound();
            }

            DetailedMovieResponse movieToReturn = new DetailedMovieResponse
            {
                Id = movieFromDatabase.Id,
                Title = movieFromDatabase.Title,
                Description = movieFromDatabase.Description,
                ActorIds = movieFromDatabase.MovieActors
                    .Select(x => x.MovieId).ToList(),
                DirectorIds = movieFromDatabase.MovieDirectors
                    .Select(x => x.DirectorId).ToList(),
                ProducerIds = movieFromDatabase.MovieProducers
                    .Select(x => x.ProducerId).ToList(),
            };

            return Ok(movieToReturn);
        }

        [HttpPost("api/movies")]
        public ActionResult<DetailedMovieResponse> Create([FromBody] MovieCreateRequest request)
        {
            Movie? newMovie = new Movie
            {
                Title = request.Title,
                Description = request.Description,
            };
            _context.Movies.Add(newMovie);

            List<MovieActor> movieActors = new List<MovieActor>();
            foreach(int actorId in request.ActorIds)
            {
                movieActors.Add(new MovieActor
                {
                    Movie = newMovie,
                    ActorId = actorId,
                });
            }
            _context.MoviesActors.AddRange(movieActors);

            List<MovieDirector> movieDirectors = new List<MovieDirector>();
            foreach(int directorId in request.DirectorIds)
            {
                movieDirectors.Add(new MovieDirector
                {
                    Movie = newMovie,
                    DirectorId = directorId,
                });
            }
            _context.MovieDirectors.AddRange(movieDirectors);

            List<MovieProducer> movieProducers = new List<MovieProducer>();
            foreach(int producerId in request.ProducerIds)
            {
                movieProducers.Add(new MovieProducer
                {
                    Movie = newMovie,
                    ProducerId = producerId,
                });
            }
            _context.MovieProducers.AddRange(movieProducers);
            _context.SaveChanges();

            DetailedMovieResponse movieToReturn = new DetailedMovieResponse
            {
                Id = newMovie.Id,
                Title = newMovie.Title,
                Description = newMovie.Description,
                ActorIds = movieActors.Select(x => x.ActorId)
                    .ToList(),
                DirectorIds = movieDirectors.Select(x => x.DirectorId)
                    .ToList(),
                ProducerIds = movieProducers.Select(x => x.ProducerId)
                    .ToList()
            };

            return Created($"api/movies/{movieToReturn.Id}", movieToReturn);
        }

        [HttpPut("api/movies/{movieId}")]
        public ActionResult<DetailedMovieResponse> Update(
            [FromRoute] int movieId,
            [FromBody] MovieUpdateRequest request)
        {
            Movie? movieToUpdate = _context.Movies
                .Include(x => x.MovieActors)
                .Include(x => x.MovieDirectors)
                .Include(x => x.MovieProducers)
                .FirstOrDefault(x => x.Id == movieId);

            if (movieToUpdate == null)
            {
                return NotFound();
            }

            List<int> actorIdsInDatabaseForMovie = _context.MoviesActors
                .Where(x => x.MovieId == movieId)
                .Select(x => x.ActorId)
                .ToList();

            List<int> actorIdsToRemoveFromMovie = actorIdsInDatabaseForMovie
                .Where(x => request.ActorIds.All(y => y != x))
                .ToList();

            List<MovieActor> movieActorsToRemove = _context.MoviesActors
                .Where(x => x.MovieId == movieId && actorIdsInDatabaseForMovie.Contains(x.ActorId))
                .ToList();
            _context.MoviesActors.RemoveRange(movieActorsToRemove);

            List<int> actorIdsToAddForMovie = request.ActorIds
                .Where(x => actorIdsInDatabaseForMovie.All(y => y != x))
                .ToList();

            List<MovieActor> movieActorsToAdd = actorIdsToAddForMovie
                .Select(actorId => new MovieActor
                {
                    MovieId = movieId,
                    ActorId = actorId
                })
                .ToList();
            
            List<int> directorIdsInDatabaseForMov
        }
    }
}
