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
        public async Task<IActionResult> CreateDelivery([FromBody] DeliveryDto deliveryDto)
        {
            await _deliveryService.CreateDeliveryAsync(deliveryDto);
            return CreatedAtAction(nameof(GetByIdDelivery), new { id = deliveryDto.IdDelivery }, deliveryDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            await _deliveryService.DeleteDeliveryAsync(id);
            return NoContent();
        }
    }
}
