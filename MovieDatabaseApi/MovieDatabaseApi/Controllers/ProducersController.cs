using Microsoft.AspNetCore.Mvc;
using MovieDatabaseApi.Data;
using MovieDatabaseApi.Data.Entities;
using MovieDatabaseApi.Migrations;
using MovieDatabaseApi.Requests;
using MovieDatabaseApi.Responses;

namespace MovieDatabaseApi.Controllers
{
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly DataContext _context;

        public ProducersController(DataContext dataContext) 
        {
            _context = dataContext;
        }

        [HttpGet("api/producers")]
        public ActionResult<List<ProducerResponse>> GetAll()
        {
            List<ProducerResponse> producersToReturn = _context.Producers
                .Select(x => new ProducerResponse
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                })
                .ToList();

            return Ok(producersToReturn);
        }

        [HttpGet("api/producers/{producerId}")]
        public ActionResult<ProducerResponse> Get([FromRoute] int producerId)
        {
            Producer? producerFromDatatbase = _context.Producers
                .FirstOrDefault(x => x.Id == producerId);

            if (producerFromDatatbase == null)
            {
                return NotFound();
            }

            ProducerResponse producerToReturn = new ProducerResponse 
            { 
                Id = producerFromDatatbase.Id,
                FirstName= producerFromDatatbase.FirstName,
                LastName= producerFromDatatbase.LastName,
            };

            return Ok(producerToReturn);
        }

        [HttpPost("api/producers")]
        public ActionResult<ProducerResponse> Create(ProducerCreateRequest request) 
        {
            Producer producerToAdd = new Producer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            _context.Producers.Add(producerToAdd);
            _context.SaveChanges();

            ProducerResponse producerToReturn = new ProducerResponse
            {
                Id = producerToAdd.Id,
                FirstName = producerToAdd.FirstName,
                LastName = producerToAdd.LastName,
            };

            return Created($"api/producers/{producerToReturn.Id}", producerToReturn);
        }

        [HttpPut("api/producers/{prodcuerId}")]
        public ActionResult<ProducerResponse> Update(
            [FromRoute] int prodcuerId, 
            [FromBody] ProducerUpdateRequest request)
        {
            Producer? producerToUpdate = _context.Producers
                .FirstOrDefault(x => x.Id == prodcuerId);
            
            if (producerToUpdate == null)
            {
                return NotFound();
            }

            producerToUpdate.FirstName = request.FirstName;
            producerToUpdate.LastName = request.LastName;

            _context.SaveChanges();

            ProducerResponse producerToReturn = new ProducerResponse
            {
                Id = producerToUpdate.Id,
                FirstName = producerToUpdate.FirstName,
                LastName = producerToUpdate.LastName,
            };

            return Ok(producerToReturn);
        }

        [HttpDelete("api/producers/{producerId}")]
        public ActionResult Delete([FromRoute]int producerId) 
        {
            Producer? prodcuerToDelete = _context.Producers
                .FirstOrDefault(x => x.Id == producerId);

            if (prodcuerToDelete == null)
            {
                return NotFound();
            }

            _context.Producers.Remove(prodcuerToDelete);
            _context.SaveChanges();

            return Ok();
        }
    }
}
