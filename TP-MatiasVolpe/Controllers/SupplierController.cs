using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace TP_MatiasVolpe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var suppliers = await _supplierService.GetAllAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier = await _supplierService.GetByIdAsync(id);
            if (supplier == null) return NotFound();
            return Ok(supplier);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplierDto supplierDto)
        {
            var createdSupplier = await _supplierService.CreateAsync(supplierDto);

            return CreatedAtAction(nameof(GetById), new { id = createdSupplier.IdSupplier }, createdSupplier);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool deleted = await _supplierService.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound(new { message = $"No se encontró un proveedor con ID = {id}." });
                }

                return Ok(new { message = $"El proveedor con ID = {id} fue eliminado correctamente." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", details = ex.Message });
            }
        }

    }
}
