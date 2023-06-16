using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechDevices_API.Data;
using TechDevices_API.Models;

namespace TechDevices_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly TechDevices_APIContext _context;

        public DevicesController(TechDevices_APIContext context)
        {
            _context = context;
        }

        // GET: api/Devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Devices>>> GetDevices()
        {
          if (_context.Devices == null)
          {
              return NotFound();
          }
            return await _context.Devices.ToListAsync();
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Devices>> GetDevices(int id)
        {
          if (_context.Devices == null)
          {
              return NotFound();
          }
            var devices = await _context.Devices.FindAsync(id);

            if (devices == null)
            {
                return NotFound();
            }

            return devices;
        }

        // PUT: api/Devices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevices(int id, Devices devices)
        {
            if (id != devices.Id)
            {
                return BadRequest();
            }

            _context.Entry(devices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DevicesExists(id))
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

        // POST: api/Devices
        [HttpPost]
        public async Task<ActionResult<Devices>> PostDevices(Devices devices)
        {
          if (_context.Devices == null)
          {
              return Problem("Entity set 'TechDevices_APIContext.Devices'  is null.");
          }
            _context.Devices.Add(devices);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevices", new { id = devices.Id }, devices);
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevices(int id)
        {
            if (_context.Devices == null)
            {
                return NotFound();
            }
            var devices = await _context.Devices.FindAsync(id);
            if (devices == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(devices);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DevicesExists(int id)
        {
            return (_context.Devices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
