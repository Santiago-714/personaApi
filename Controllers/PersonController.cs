using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personaApi.Data;
using personaApi.Model;

namespace personaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PersonController: ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly DataContext _context;

        public PersonController(ILogger<PersonController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetPerson")] //Listar todas las personas
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return await _context.Persons.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetPersonById")] //Obtener persona
        public async Task<ActionResult<Person>> GetPersonById(int id)
        {
            var person = await _context.Persons.FindAsync(id);

            if(person == null){
                return NotFound(); //Status 404
            }

            return person;
        }

        [HttpPost] 
        public async Task<ActionResult<Person>> Post(Person person) //Regresar la persona creada
        {
            _context.Add(person);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetPersonById", new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Person person)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            if(id != person.Id){
                return BadRequest();
            }

            //_context.Entry(person).State = EntityState.Modified;
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> Delete(int id)
        {
            var person = await _context.Persons.FindAsync(id);

            if(person == null){
                return NotFound();
            }

            _context.Persons.Remove(person); //Tambien _context.Remove(person); 
            await _context.SaveChangesAsync();


            return person;
        }

        

    }
}