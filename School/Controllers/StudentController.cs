using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.DTOs.ClassDTOs;
using School.DTOs.StudentDTOs;
using School.Models;
using School.UnitOfWorks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public StudentController(UnitOfWork unit, IMapper mapper) {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult getAll() {
            var students = unit.StudentRepo.getAllWithClass();
            if (students == null) {
                return NotFound("No Students Found");
            }
            else
            {
                List<displayStudentDTO> classDTO = mapper.Map<List<displayStudentDTO>>(students);
                return Ok(classDTO);
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult getById(int id)
        {
            var students = unit.StudentRepo.getByIdWithClass(id);
            if (students == null)
            {
                return NotFound("Invalid Student Id");
            }
            else
            {
                displayStudentDTO stdDTO = mapper.Map<displayStudentDTO>(students);
                return Ok(stdDTO);
            }
        }


        [HttpGet("{name}")]
        public IActionResult getByName(string name)
        {
            var students = unit.StudentRepo.getByNameWithClass(name);
            if (students == null)
            {
                return NotFound("Invalid Studen Name");
            }
            else
            {
                displayStudentDTO classDTO = mapper.Map<displayStudentDTO>(students);
                return Ok(classDTO);
            }
        }


        [HttpGet("ByClassId/{classId}")]
        public IActionResult getStudentsByClassId(int classId) {

            var cls = unit.ClassRepo.getById(classId);
            if (cls == null)
            {
                return NotFound("Invalid Class Id");
            }
            else
            {

                var students = unit.StudentRepo.getStudentsByClassId(classId);
                if (students == null)
                {
                    return NotFound("This class has no students");
                }
                else
                {
                    List<displayStudentDTO> stdDTO = mapper.Map<List<displayStudentDTO>>(students);
                    return Ok(stdDTO);
                }

            }
        }

        [HttpGet("ByClassName/{className}")]
        public IActionResult getStudentsByClassName(string className)
        {

            var cls = unit.ClassRepo.getClassByName(className);
            if (cls == null)
            {
                return NotFound("Invalid Class name");
            }
            else
            {

                var students = unit.StudentRepo.getStudentsByClassName(className);
                if (students == null)
                {
                    return NotFound("This class has no students");
                }
                else
                {
                    List<displayStudentDTO> stdDTO = mapper.Map<List<displayStudentDTO>>(students);
                    return Ok(stdDTO);
                }

            }
        }




        [HttpPost]
        public IActionResult addStudent(addStudentDTO studentDTO)
        {
            Student student = mapper.Map<Student>(studentDTO);
            unit.StudentRepo.add(student);
            unit.save();
            displayStudentDTO stdDTO = mapper.Map<displayStudentDTO>(unit.StudentRepo.getByIdWithClass(student.Id));
            return Ok(stdDTO);
        }


        [HttpPut("{id}")]
        public IActionResult editStudent(int id, addStudentDTO studentDTO)
        {
            var student = unit.StudentRepo.getByIdWithClass(id);
            if (student == null)
            {
                return NotFound("Invalid student Id");
            }
            else
            {
                mapper.Map(studentDTO, student);
                unit.StudentRepo.edit(student);
                unit.save();
                Student std = unit.StudentRepo.getByIdWithClass(id);
                displayStudentDTO stdDTO = mapper.Map<displayStudentDTO>(std);
                return Ok(stdDTO);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult deleteStudent(int id) {
            var student = unit.StudentRepo.getByIdWithClass(id);
            if( student == null)
            {
                return NotFound();
            }
            else
            {
                displayStudentDTO stdDto = mapper.Map<displayStudentDTO>(student);
                unit.StudentRepo.delete(id);
                unit.save();
                return Ok(stdDto);
            }
        }

        [HttpGet("getByParentId/{parentId:int}")]
        public IActionResult getByParentId(int parentId) {
            var prnt = unit.ParentRepo.getById(parentId);
            if(prnt == null)
            {
                return NotFound("No Parents With this Id");
            }
            else
            {
                var stds = unit.StudentRepo.getByPrntId(parentId);
                List<displayStudentDTO> stdDto = mapper.Map<List<displayStudentDTO>>(stds);
                return Ok(stdDto);
            }
        }



    }
}
