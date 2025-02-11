using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Create([FromBody] CreatePersonDto dto)
        {
            var currentUserRole = int.Parse(User.FindFirst(ClaimTypes.Role)?.Value ?? "1");

            if (dto.Role >= 2 && currentUserRole != 3)
            {
                return Unauthorized();
            }

            try
            {
                _personService.Create(dto);
                return Ok(new { message = "Se creó correctamente la persona." });
            }

            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error.", details = ex.Message });
            }
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("{id}/password")]
        public IActionResult ChangePassword(int id, [FromBody] string newPassword)
        {
            bool updated = _personService.ChangePassword(id, newPassword);

            if (!updated)
            {
                return NotFound(new { message = $"No se encontró una persona con ID = {id}." });
            }

            return Ok(new { message = "Contraseña actualizada correctamente." });
        }

        [HttpPut("{id}/shift")]
        public IActionResult ChangeShift(int id, [FromBody] string newShift)
        {
            try
            {
                bool updated = _personService.ChangeShift(id, newShift);

                if (!updated)
                {
                    return NotFound(new { message = $"No se encontró una persona con ID = {id}." });
                }

                return Ok(new { message = "Turno actualizado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "2,3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var currentUserRole = int.Parse(User.FindFirst(ClaimTypes.Role)?.Value ?? "1");

            var personToDelete = _personService.GetById(id);
            if (personToDelete == null)
            {
                return NotFound(new { message = $"No se encontró una persona con ID = {id}." });
            }

            if (personToDelete.Role >= 2 && currentUserRole != 3)
            {
                return Unauthorized();
            }

            bool deleted = _personService.Delete(id);
            if (!deleted)
            {
                return NotFound(new { message = $"No se encontró una persona con ID = {id}." });
            }

            return Ok(new { message = $"La persona con ID = {id} fue eliminada correctamente." });
        }
    }
}
