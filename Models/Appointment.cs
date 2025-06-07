using System;

namespace AutoCareAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string ServiceType { get; set; }
        public string Notes { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
