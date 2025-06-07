using AutoCareAPI.Data;
using AutoCareAPI.DTOs;
using AutoCareAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoCareAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly AutoCareDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(AutoCareDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(_mapper.Map<List<CustomerDto>>(customers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CreateCustomerDto createDto)
        {
            var customer = _mapper.Map<Customer>(createDto);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var customerDto = _mapper.Map<CustomerDto>(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customerDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CreateCustomerDto updateDto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            _mapper.Map(updateDto, customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
