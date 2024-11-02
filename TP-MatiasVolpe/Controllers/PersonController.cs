using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TP_MatiasVolpe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var persons = _personService.GetAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var person = _personService.GetById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePersonDto dto)
        {
            _personService.Create(dto);

            var person = _personService.GetAll().LastOrDefault(); 

            if (person == null)
            {
                return BadRequest("Failed to create person.");
            }

            return CreatedAtAction(nameof(GetById), new { id = person.IdPerson }, person);
        }

        [HttpPut("{id}/password")]
        public IActionResult ChangePassword(int id, [FromBody] string newPassword)
        {
            _personService.ChangePassword(id, newPassword);
            return NoContent();
        }

        [HttpPut("{id}/shift")]
        public IActionResult ChangeShift(int id, [FromBody] string newShift)
        {
            _personService.ChangeShift(id, newShift);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }

}
