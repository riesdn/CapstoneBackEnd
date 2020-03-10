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
    public class RequestLinesController: ControllerBase {
        private readonly CapContext _context;

        public RequestLinesController(CapContext context) {
            _context = context;
        }

        private async Task CalcRequestTotal(int requestId) {

            var request = await _context.Requests.FindAsync(requestId);

            request.Total = _context.RequestLines.Include(l => l.Product).Where(l => l.RequestId == requestId).Sum(l => l.Quantity * l.Product.Price);

            _context.Entry(request).State = EntityState.Modified;
            
            try {
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                throw;
            }

            return;
        }

        // GET: api/RequestLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestLine>>> GetRequestLine() {
            return await _context.RequestLines.ToListAsync();
        }

        // GET: api/RequestLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLine>> GetRequestLine(int id) {
            var requestLine = await _context.RequestLines.FindAsync(id);

            if (requestLine == null) {
                return NotFound();
            }

            return requestLine;
        }

        // PUT: api/RequestLines/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestLine(int id, RequestLine requestLine) {
            if (id != requestLine.Id) {
                return BadRequest();
            }

            /* --- THIS DOESNT WORK, SYSTEM CAN'T TRACK THE SAME INSTANCE TWICE
            var reqLineBeforeUpdate = await _context.RequestLines.FindAsync(id);
            int oldRequestId = reqLineBeforeUpdate.RequestId;
            // if RequestId is changed, the new linked Request will get it's Total calculated correctly,
            // but the old Request will not. Thus, store old reqId and also recalculate the old Request's total
            // so that both Requests will be correct.
            */
            _context.Entry(requestLine).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
                /*if (oldRequestId != requestLine.RequestId) {
                    await CalcRequestTotal(oldRequestId);
                }*/
                await CalcRequestTotal(requestLine.RequestId);
            } catch (DbUpdateConcurrencyException) {
                if (!RequestLineExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RequestLines
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RequestLine>> PostRequestLine(RequestLine requestLine) {
            _context.RequestLines.Add(requestLine);
            await _context.SaveChangesAsync();
            await CalcRequestTotal(requestLine.RequestId);
            return CreatedAtAction("GetRequestLine", new { id = requestLine.Id }, requestLine);
        }

        // DELETE: api/RequestLines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RequestLine>> DeleteRequestLine(int id) {
            
            var requestLine = await _context.RequestLines.FindAsync(id);
            if (requestLine == null) {
                return NotFound();
            }

            int requestId = requestLine.RequestId;
            //capture requestId before requestline is removed so then Request.Total can be recalculated after removal appropriately

            _context.RequestLines.Remove(requestLine);
            await _context.SaveChangesAsync();
            await CalcRequestTotal(requestId);
            return requestLine;
        }

        private bool RequestLineExists(int id) {
            return _context.RequestLines.Any(e => e.Id == id);
        }
    }
}
