using AutoMapper;
using Web_API_Rate_Limiting.Controllers.NewFolder.Request;
using Web_API_Rate_Limiting.Controllers.NewFolder.Response;
using Web_API_Rate_Limiting.Models;

namespace Web_API_Rate_Limiting.Utilitis.MapperProfiles
{
    public class AutoMapperProfiler:Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<Student, StudentRequest>().ReverseMap();
            CreateMap<Student, StudentResponse>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department.Id))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
        }
    }
}
