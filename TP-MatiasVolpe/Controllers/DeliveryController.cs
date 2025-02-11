using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace TP_MatiasVolpe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdDelivery(int id)
        {
            var delivery = await _deliveryService.GetByIdDeliveryAsync(id);
            return delivery != null ? Ok(delivery) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDelivery([FromBody] CreateDeliveryDto deliveryDto)
        {
            try
            {
                var createdDelivery = await _deliveryService.CreateDeliveryAsync(deliveryDto);
                return CreatedAtAction(nameof(GetByIdDelivery), new { id = createdDelivery.IdDelivery }, createdDelivery);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            bool deleted = await _deliveryService.DeleteDeliveryAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = $"No se encontró una entrega con ID = {id}." });
            }

            return Ok(new { message = $"La entrega con ID = {id} fue eliminada correctamente." });
        }

    }
}
