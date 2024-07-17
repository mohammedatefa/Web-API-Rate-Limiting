using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Web_API_Rate_Limiting.Controllers.NewFolder.Response;
using Web_API_Rate_Limiting.Models;
using Web_API_Rate_Limiting.Repository;
using Web_API_Rate_Limiting.Utilitis.UOW;

namespace Web_API_Rate_Limiting.Controllers.StudentController
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("SlidingWindowLimiterPolicy")]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Student> _repository;

        public StudentController(IUnitOfWork unitOfWork, ILogger<StudentController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _repository = unitOfWork.Repository<Student>();
        }

        [HttpGet("AllStudents")]
        public async Task<IActionResult> GetAll()
        {
            var students =await  _repository.GetAllAsync("Department");
            var resualt = _mapper.Map<IEnumerable<StudentResponse>>(students);
            return Ok(new
            {
                Message = "Succeded",
                Data = resualt
            });

        }
    }
}
