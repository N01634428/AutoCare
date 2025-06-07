using System;

namespace AutoCareAPI.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string ServiceType { get; set; }
        public string Notes { get; set; }
        public int CustomerId { get; set; }
    }
}
