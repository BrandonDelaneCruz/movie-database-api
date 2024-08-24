using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MovieDatabaseApi.Data;
using MovieDatabaseApi.Data.Entities;
using MovieDatabaseApi.Requests;
using MovieDatabaseApi.Responses;

namespace MovieDatabaseApi.Controllers
{
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly DataContext _context;

        public DirectorsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("api/directors")]
        public ActionResult<List<DirectorResponse>> GetAll()
        {
            List<DirectorResponse> directorsToReturn = _context.Directors
                .Select(x => new DirectorResponse
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                })
                .ToList();

            return Ok(directorsToReturn);
        }

        [HttpGet("api/directors/{directorsId}")]
        public ActionResult<DirectorResponse> Get(int directorsId)
        {
            Director? directorFromDatabase = _context.Directors
                .FirstOrDefault(x => x.Id == directorsId);

            if (directorFromDatabase == null)
            {
                return NotFound();
            }

            DirectorResponse directorToReturn = new DirectorResponse
            {
                Id = directorFromDatabase.Id,
                FirstName = directorFromDatabase.FirstName,
                LastName = directorFromDatabase.LastName,
            };

            return Ok(directorToReturn);
        }

        [HttpPost("api/directors")]
        public ActionResult<DirectorResponse> Create([FromBody] DirectorCreateRequest request)
        {
            Director directorToAdd = new Director
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            _context.Directors.Add(directorToAdd);
            _context.SaveChanges();

            DirectorResponse directorToReturn = new DirectorResponse
            {
                Id = directorToAdd.Id,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            return Created($"api/directors/{directorToReturn.Id}", directorToReturn);
        }

        [HttpPut("api/directors/{directorId}")]
        public ActionResult<DirectorResponse> Update([FromRoute] int directorId, [FromBody] DirectorUpdateRequest request)
        {
            Director? directorToUpdate = _context.Directors
                .FirstOrDefault(x => x.Id == directorId);

            if (directorToUpdate == null)
            {
                return NotFound();
            }

            directorToUpdate.FirstName = request.FirstName;
            directorToUpdate.LastName = request.LastName;

            _context.SaveChanges();

            DirectorResponse directorToReturn = new DirectorResponse
            {
                Id = directorToUpdate.Id,
                FirstName = directorToUpdate.FirstName,
                LastName = directorToUpdate.LastName
            };

            return Ok(directorToReturn);
        }

        [HttpDelete("api/directors/{directorId}")]
        public ActionResult Delete([FromRoute] int directorId)
        {
            Director? directorToDelete = _context.Directors
                .FirstOrDefault(x => x.Id == directorId);

            if (directorToDelete == null)
            {
                return NotFound();
            }

            _context.Directors.Remove(directorToDelete);
            _context.SaveChanges();

            return Ok();
        }
    }
}
