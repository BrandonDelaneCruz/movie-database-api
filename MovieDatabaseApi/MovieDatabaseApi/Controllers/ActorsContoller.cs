using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Identity.Client;
using MovieDatabaseApi.Data;
using MovieDatabaseApi.Data.Entities;
using MovieDatabaseApi.Requests;
using MovieDatabaseApi.Responses;
using System.Reflection.Metadata.Ecma335;

namespace MovieDatabaseApi.Controllers
{
    [ApiController]
    public class ActorsContoller : ControllerBase
    {
        private readonly DataContext _context;

        public ActorsContoller(DataContext context)
        {
            _context = context;
        }

        [HttpGet("api/actors")]
        public ActionResult<List<ActorResponse>> GetAll()
        {
            List<ActorResponse> actorsToReturn = _context.Actors
                .Select(x => new ActorResponse
                {
                    Id = x.Id,
                    FirstName= x.FirstName,
                    LastName= x.LastName,
                })
                .ToList();

            return Ok(actorsToReturn);
        }

        [HttpGet("api/actors/{actorId}")]
        public ActionResult<ActorResponse> Get([FromRoute] int actorId)
        {
            Actor? actorFromDatabase = _context.Actors
                .FirstOrDefault(x => x.Id == actorId);

            if (actorFromDatabase == null)
            {
                return NotFound();
            }

            ActorResponse actorToReturn = new ActorResponse
            {
                Id = actorFromDatabase.Id,
                FirstName = actorFromDatabase.FirstName,
                LastName = actorFromDatabase.LastName,
            };

            return Ok(actorToReturn);
        }

        [HttpPost("api/actors")]
        public ActionResult<ActorResponse> Create([FromBody] ActorCreateRequest request)
        {
            Actor actorToCreate = new Actor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            _context.Actors.Add(actorToCreate);
            _context.SaveChanges();

            ActorResponse actorToRetrun = new ActorResponse
            {
                Id = actorToCreate.Id,
                FirstName = actorToCreate.FirstName,
                LastName = actorToCreate.LastName
            };

            return Created($"api/actors/{actorToRetrun.Id}", actorToRetrun);
        }

        [HttpPut("api/actors/{actorId}")]
        public ActionResult<ActorResponse> Update(
            [FromRoute] int actorId, 
            [FromBody] ActorUpdateRequest request)
        {
            Actor? actorFromDatabase = _context.Actors
                .FirstOrDefault(a => a.Id == actorId);

            if (actorFromDatabase == null)
            {
                return NotFound();
            }

            actorFromDatabase.FirstName = request.FirstName;
            actorFromDatabase.LastName = request.LastName;

            _context.SaveChanges();

            ActorResponse actorToReturn = new ActorResponse
            {
                Id = actorFromDatabase.Id,
                FirstName = actorFromDatabase.FirstName,
                LastName = actorFromDatabase.LastName
            };

            return Ok(actorToReturn);
        }

        [HttpDelete("api/actors/{actorId}")]
        public ActionResult Delete(int actorId)
        {
            Actor? actorToDelete = _context.Actors
                .FirstOrDefault(x => x.Id == actorId);

            if (actorToDelete == null)
            {
                return NotFound();
            }

            _context.Actors.Remove(actorToDelete);
            _context.SaveChanges();

            return Ok();
        }
    }
}