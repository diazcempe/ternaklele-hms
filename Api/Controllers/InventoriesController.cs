using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using Api.Services;
using Common.Dtos;
using Common.ViewModels.Inventory;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ExtendedControllerBase
    {
        private readonly TernakLeleHmsContext _context;
        private readonly IInventoriesService _service;
        private readonly ILogger<InventoriesController> _logger;

        public InventoriesController(TernakLeleHmsContext context, IInventoriesService service, ILogger<InventoriesController> logger)
        {
            _context = context;
            _service = service;
            _logger = logger;
        }

        // GET: api/Inventories
        [HttpGet]
        public async Task<IEnumerable<InventoryDto>> GetInventories()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Inventories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventory([FromRoute] long id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var inventoryDto = await _service.GetByIdAsync(id);

            if (inventoryDto == null)
                return NotFound();

            return Ok(inventoryDto);
        }

        // PUT: api/Inventories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory([FromRoute] long id, [FromBody] Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inventory.InventoryId)
            {
                return BadRequest();
            }

            _context.Entry(inventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Inventories
        [HttpPost]
        public async Task<IActionResult> PostInventory([FromBody] InventoryCreateVm vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation($"Inventory Create {vm}");
            return ProcessOperationResult(await _service.CreateAsync(vm));
        }

        // DELETE: api/Inventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory([FromRoute] long id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation($"Inventory Delete. Id: {id}");
            return ProcessOperationResult(await _service.DeleteAsync(id));
        }

        private bool InventoryExists(long id)
        {
            return _context.Inventories.Any(e => e.InventoryId == id);
        }
    }
}