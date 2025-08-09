﻿using DessertsApi.Data;
using DessertsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DessertsApi.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class ProductsController : ControllerBase
        {
            private readonly AppDbContext _context;

            public ProductsController(AppDbContext context)
            {
                _context = context;
            }

            // GET: api/products
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
            {
                return await _context.Products.ToListAsync();
            }

            // GET: api/products/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Product>> GetProduct(int id)
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null) return NotFound();

                return product;
            }

            // POST: api/products
            [HttpPost]
            public async Task<ActionResult<Product>> PostProduct(Product product)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduct), new { id = product.id }, product);
            }

            // PUT: api/products/5
            [HttpPut("{id}")]
            public async Task<IActionResult> PutProduct(int id, Product product)
            {
                if (id != product.id) return BadRequest();

                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }

            // DELETE: api/products/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteProduct(int id)
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null) return NotFound();

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
    }
