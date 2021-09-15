using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.StoreApplication.Domain.Models;

namespace Project1.StoreApplication.Business.Controllers
{
    [Route("html/api/[controller]")]
    [ApiController]
    public class LocationInventoriesController : ControllerBase
    {
        private readonly Kyles_Pizza_ShopContext _context;

        public LocationInventoriesController(Kyles_Pizza_ShopContext context)
        {
            _context = context;
        }

        // GET: api/LocationInventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationInventory>>> GetLocationInventories()
        {
            return await _context.LocationInventories.ToListAsync();
        }

        // GET: api/LocationInventories/5
        [HttpGet("{LocationId}")]
        public async Task<ActionResult<IEnumerable<LocationInventory>>> GetLocationInventory(int LocationId)
        {
            var locationInventory = _context.LocationInventories.FromSqlRaw<LocationInventory>($"select * from LocationInventory where LocationId = {LocationId} order by ProductId").ToList();

            if (locationInventory == null)
            {
                return NotFound();
            }

            return locationInventory;
        }

        // PUT: api/LocationInventories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocationInventory(int id, LocationInventory locationInventory)
        {
            if (id != locationInventory.Id)
            {
                return BadRequest();
            }

            _context.Entry(locationInventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationInventoryExists(id))
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

        // POST: api/LocationInventories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LocationInventory>> PostLocationInventory(LocationInventory locationInventory)
        {
            _context.LocationInventories.Add(locationInventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocationInventory", new { id = locationInventory.Id }, locationInventory);
        }

        // DELETE: api/LocationInventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationInventory(int id)
        {
            var locationInventory = await _context.LocationInventories.FindAsync(id);
            if (locationInventory == null)
            {
                return NotFound();
            }

            _context.LocationInventories.Remove(locationInventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationInventoryExists(int id)
        {
            return _context.LocationInventories.Any(e => e.Id == id);
        }
    }
}
