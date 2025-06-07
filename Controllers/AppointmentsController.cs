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
    public class AppointmentsController : ControllerBase
    {
        private readonly AutoCareDbContext _context;
        private readonly IMapper _mapper;

        public AppointmentsController(AutoCareDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointments()
        {
            var appointments = await _context.Appointments.ToListAsync();
            return Ok(_mapper.Map<List<AppointmentDto>>(appointments));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            return Ok(_mapper.Map<AppointmentDto>(appointment));
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> CreateAppointment(CreateAppointmentDto createDto)
        {
            // Check if customer exists
            var customerExists = await _context.Customers.AnyAsync(c => c.Id == createDto.CustomerId);
            if (!customerExists)
                return BadRequest("Customer does not exist.");

            var appointment = _mapper.Map<Appointment>(createDto);
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            var appointmentDto = _mapper.Map<AppointmentDto>(appointment);
            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointmentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, CreateAppointmentDto updateDto)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            // Check if customer exists
            var customerExists = await _context.Customers.AnyAsync(c => c.Id == updateDto.CustomerId);
            if (!customerExists)
                return BadRequest("Customer does not exist.");

            _mapper.Map(updateDto, appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
