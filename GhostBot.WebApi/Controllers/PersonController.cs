using Microsoft.AspNetCore.Mvc; //To use [Route], [ApiController], ControllerBase and so on.
using GhostBot.EntityModels;
using GhostBot.WebApi.Repositories; //To use IPersonRepository

namespace GhostBot.WebApi.Controllers
{
    //Base address: api/person
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _repo;

        //constructor injects repository registered in Program.cs
        public PersonController(IPersonRepository repo)
        {
            _repo = repo;
        }

        //Get: api/person
        //Get: api/person/?city=[city]
        //This will always return a list of persons (but it might be empty)
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Person>))]
        public async Task<IEnumerable<Person>> GetPersons(string? city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return await _repo.RetrieveAllAsync();
            }
            else 
            {
                return (await _repo.RetrieveAllAsync()).Where(person => person.City == city);
            }
        }

        //Get: api/person/[id]
        [HttpGet("{id}", Name = nameof(GetPerson))] //Named route
        [ProducesResponseType(200, Type = typeof(Person))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPerson(string id)
        {
            Person? p = await _repo.RetrieveAsync(id);
            if (p == null)
            {
                return NotFound(); // 404
            }
            return Ok(p); //200 OK
        }

        //Post: api/person
        //Body: Person (JSON, XML)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Person))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Person p)
        {
            if (p == null)
            {
                return BadRequest(); //400
            }
            Person? addedPerson = await _repo.CreateAsync(p);
            if (addedPerson == null)
            {
                return BadRequest("Repository failed to created person.");
            }
            return CreatedAtRoute( //201 created
                routeName: nameof(GetPerson),
                routeValues: new { id = addedPerson.PersonId.ToLower() },
                value: addedPerson
            );
        }

        //Put: api/person/[id]
        //Body: Person (JSON, XML)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(string id, [FromBody] Person p)
        {
            id = id.ToUpper();
            p.PersonId = p.PersonId.ToUpper();

            if (p == null || p.PersonId != id)
            {
                return BadRequest(); //400
            }
            Person? existing = await _repo.RetrieveAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found.
            }
            await _repo.UpdateAsync(p);
            return new NoContentResult(); //204
        }

        //Delete: api/person/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == "bad")
            {
                ProblemDetails problemDetails = new()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://localhost:5151/person/failed-to-delete",
                    Title = $"Person ID {id} found but failed to delete.",
                    Detail = "More details like City, Region and so on.",
                    Instance = HttpContext.Request.Path
                };
                return BadRequest(problemDetails); //400 Bad Request
            }
            
            Person? existing = await _repo.RetrieveAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404
            }
            bool? deleted = await _repo.DeleteAsync(id);
            if (deleted.HasValue && deleted.Value) //Short circuit AND.
            {
                return new NoContentResult();
            }
            return BadRequest( //400
                $"Person {id} was found but failed to delete."
            );
        }
    }
}