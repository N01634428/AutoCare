using System.Collections.Generic;

namespace AutoCareAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
