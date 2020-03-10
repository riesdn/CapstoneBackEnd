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
    public class ProductOrdersController: ControllerBase {
        private readonly CapContext _context;

        public ProductOrdersController(CapContext context) {
            _context = context;
        }

        // GET: api/ProductOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductOrder>>> GetProductOrder() {
            return await _context.ProductOrders.ToListAsync();
        }

        // GET: api/ProductOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOrder>> GetProductOrder(int id) {
            var productOrder = await _context.ProductOrders.FindAsync(id);

            if (productOrder == null) {
                return NotFound();
            }

            return productOrder;
        }

        // PUT: api/ProductOrders/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductOrder(int id, ProductOrder productOrder) {
            if (id != productOrder.Id) {
                return BadRequest();
            }

            _context.Entry(productOrder).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!ProductOrderExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductOrders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ProductOrder>> PostProductOrder(ProductOrder productOrder) {
            _context.ProductOrders.Add(productOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductOrder", new { id = productOrder.Id }, productOrder);
        }

        // DELETE: api/ProductOrders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductOrder>> DeleteProductOrder(int id) {
            var productOrder = await _context.ProductOrders.FindAsync(id);
            if (productOrder == null) {
                return NotFound();
            }

            _context.ProductOrders.Remove(productOrder);
            await _context.SaveChangesAsync();

            return productOrder;
        }

        private bool ProductOrderExists(int id) {
            return _context.ProductOrders.Any(e => e.Id == id);
        }
    }
}
