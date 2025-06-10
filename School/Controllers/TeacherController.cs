using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.DTOs.TeacherDTO;
using School.DTOs.TeacherDTOs;
using School.Models;
using School.UnitOfWorks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public TeacherController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }


        [HttpGet]
        public IActionResult getAllTeachers()
        {
            var teachers = unit.TeacherRepo.getAll();
            if (teachers == null)
            {
                return NotFound();
            }
            else
            {
                List<displayTeacherDTO> teachDTO = mapper.Map<List<displayTeacherDTO>>(teachers);
                return Ok(teachDTO);
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult getTeacherById(int id) {
            var teach = unit.TeacherRepo.getById(id);
            if(teach == null)
            {
                return NotFound();
            }
            else
            {
                displayTeacherDTO teachDTO = mapper.Map<displayTeacherDTO>(teach);
                return Ok(teachDTO);
            }
        }

        [HttpGet("{name}")]
        public IActionResult getTeacherByName(string name) {
            var teach = unit.TeacherRepo.getByName(name);
            if(teach == null)
            {
                return NotFound();
            }
            else
            {
                displayTeacherDTO teachDTO = mapper.Map<displayTeacherDTO>(teach);
                return Ok(teachDTO);
            }
        }

        [HttpPost]
        public IActionResult addTeacher(addTeacherDTO teacherDTO) { 
            Teacher teacher = mapper.Map<Teacher>(teacherDTO);
            unit.TeacherRepo.add(teacher);
            unit.save();
            displayTeacherDTO teach = mapper.Map<displayTeacherDTO>(teacher);
            return Ok(teach);
        }

        [HttpPut("{id}")]
        public IActionResult editTeacher(int id , addTeacherDTO teachDTO)
        {
            var teacher = unit.TeacherRepo.getById(id);
            if( teacher == null)
            {
                return NotFound();
            }
            else
            {
                mapper.Map(teachDTO, teacher);
                unit.TeacherRepo.edit(teacher);
                unit.save();
                displayTeacherDTO tDTO = mapper.Map<displayTeacherDTO> (teacher);
                return Ok(tDTO);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult deleteTeacher(int id) {
            var teacher = unit.TeacherRepo.getById(id);
            if(teacher == null)
            {
                return NotFound();
            }
            else
            {
                displayTeacherDTO teachDTo = mapper.Map<displayTeacherDTO>(teacher);
                unit.TeacherRepo.delete(id);
                unit.save();
                return Ok(teachDTo);
            }
        }

    }
}
