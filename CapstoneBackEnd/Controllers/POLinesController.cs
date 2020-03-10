using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapstoneBackEnd.Data;
using CapstoneBackEnd.Models;

namespace CapstoneBackEnd.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class POLinesController: ControllerBase {
        private readonly CapContext _context;

        public POLinesController(CapContext context) {
            _context = context;
        }

        // GET: api/POLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<POLine>>> GetPOLine() {
            return await _context.POLines.ToListAsync();
        }

        // GET: api/POLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<POLine>> GetPOLine(int id) {
            var pOLine = await _context.POLines.FindAsync(id);

            if (pOLine == null) {
                return NotFound();
            }

            return pOLine;
        }

        // PUT: api/POLines/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPOLine(int id, POLine pOLine) {
            if (id != pOLine.Id) {
                return BadRequest();
            }

            _context.Entry(pOLine).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!POLineExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/POLines
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<POLine>> PostPOLine(POLine pOLine) {
            _context.POLines.Add(pOLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPOLine", new { id = pOLine.Id }, pOLine);
        }

        // DELETE: api/POLines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<POLine>> DeletePOLine(int id) {
            var pOLine = await _context.POLines.FindAsync(id);
            if (pOLine == null) {
                return NotFound();
            }

            _context.POLines.Remove(pOLine);
            await _context.SaveChangesAsync();

            return pOLine;
        }

        private bool POLineExists(int id) {
            return _context.POLines.Any(e => e.Id == id);
        }
    }
}
