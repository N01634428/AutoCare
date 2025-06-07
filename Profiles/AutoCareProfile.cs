using AutoCareAPI.DTOs;
using AutoCareAPI.Models;
using AutoMapper;

namespace AutoCareAPI.Profiles
{
    public class AutoCareProfile : Profile
    {
        public AutoCareProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();

            CreateMap<Appointment, AppointmentDto>();
            CreateMap<CreateAppointmentDto, Appointment>();
        }
    }
}
