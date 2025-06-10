using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.UnitOfWorks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InGeneralController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public InGeneralController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetStatistics() {
            var statistics = unit.StudentRepo.countStudents();
            return Ok(statistics);
        }

        [HttpGet("{id}")]
        public IActionResult getNumOfTeacherClasses(int id)
        {
            var teacher = unit.TeacherRepo.getById(id);
            if (teacher == null)
            {
                return NotFound("No teacher with this id");
            }
            else
            {

                return Ok(unit.TeacherRepo.numOfTeacherClasses(id));
            }
        }

        [HttpGet("getNumberOfStudents/{id}")]
        public IActionResult getNumOfTeacherStudents(int id)
        {
            var teacher = unit.TeacherRepo.getById(id);
            if (teacher == null)
            {
                return NotFound("No teacher with this id");
            }
            else
            {

                return Ok(unit.TeacherRepo.numOfStdInTeacherClasses(id));
            }
        }



    }
}
