using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.DTOs.GradeDTOs;
using School.Models;
using School.UnitOfWorks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public GradeController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult getAllGrades()
        {
            var grades = unit.GradeRepo.getAllWithSubAndStud();
            if (grades == null) { 
                return NotFound();
            }
            else
            {
                List<displayGradeDTO> gradDTO = mapper.Map<List<displayGradeDTO>>(grades);
                return Ok(gradDTO);
            }

        }

        [HttpGet("ById/{id:int}")]
        public IActionResult getGradesById(int id) { 
            var grade = unit.GradeRepo.getWithSubAndStud(id);
            if (grade == null) { 
                return NotFound();
            }
            else
            {
                displayGradeDTO grdDTO = mapper.Map<displayGradeDTO>(grade);
                return Ok(grdDTO);
            }
        }

        [HttpPost]
        public IActionResult addGrade(addGradeDTO gradeDTO) { 
            Grade grade = mapper.Map<Grade>(gradeDTO);
            unit.GradeRepo.add(grade);
            unit.save();
            var loadedGrade = unit.GradeRepo.getWithSubAndStud(grade.Id);
            displayGradeDTO gDTO = mapper.Map<displayGradeDTO>(loadedGrade);
            return Ok(gDTO);
        }

        [HttpPut]
        public IActionResult editGrade(int id , addGradeDTO gradeDTO) { 
            var grade = unit.GradeRepo.getWithSubAndStud(id);
            if (grade == null) { 
                return NotFound();
            }
            else
            {
                mapper.Map(gradeDTO, grade);
                unit.GradeRepo.edit(grade);
                unit.save();
                Grade grade2 = unit.gradeRepo.getWithSubAndStud(id);
                displayGradeDTO grdDTO = mapper.Map<displayGradeDTO> (grade2);
                return Ok(grdDTO);
            }
        }

        [HttpDelete]
        public IActionResult deleteGrade(int id) {
            var grade = unit.GradeRepo.getWithSubAndStud(id);
            if (grade == null) { 
                return NotFound();
            }
            else
            {
                displayGradeDTO gradeDTO = mapper.Map<displayGradeDTO> (grade);
                unit.GradeRepo.delete(id);
                unit.save();
                return Ok(gradeDTO);
            }

        }


        [HttpGet("ByStudentId/{studentId:int}")]
        public IActionResult getGradesByStdId(int studentId) { 
            var std= unit.StudentRepo.getById(studentId);
            if (std == null) { return NotFound("Invalis Student Id"); }
            else
            {
                var grades = unit.GradeRepo.gradesByStdId(studentId);
                if (grades == null) { return NotFound(); }
                else
                { 
                    List<displayGradeDTO> grdDTO = mapper.Map<List<displayGradeDTO>>(grades);
                    return Ok(grdDTO);
                }
            }
        }

        [HttpGet("BySubjectId/{subjectId:int}")]
        public IActionResult getGradesBtSubId(int subjectId)
        {
            var sub = unit.SubjectRepo.getById(subjectId);
            if (sub == null) { return NotFound("Invalid Subject Id"); }
            else
            {
                var grades = unit.GradeRepo.gradesBySubId(subjectId);
                if (grades == null) { return NotFound(); }
                else
                {
                    List<displayGradeDTO> grdDTO = mapper.Map<List<displayGradeDTO>>(grades);
                    return Ok(grdDTO);
                }
            }
        }


        [HttpGet("BySubjectName/{subjectName}")]
        public IActionResult getGradesBtSubName(string subjectName)
        {
            var subject = unit.SubjectRepo.getSubByName(subjectName);
            if (subject == null) { return NotFound("Invalid Subject Name"); }
            else
            {
                var grades = unit.GradeRepo.gradesBySubName(subjectName);
                if (grades == null) { return NotFound(); }
                else
                {
                    List<displayGradeDTO> grdDTO = mapper.Map<List<displayGradeDTO>>(grades);
                    return Ok(grdDTO);
                }
            }
        }

        [HttpGet("ByStudentName/{studentName}")]
        public IActionResult getGradesBtStdName(string studentName)
        {
            var std = unit.StudentRepo.getByName(studentName);
            if (std == null) { return NotFound("Invalid Student Name"); }
            else
            {
                var grades = unit.GradeRepo.gradesByStdName(studentName);
                if (grades == null) { return NotFound(); }
                else
                {
                    List<displayGradeDTO> grdDTO = mapper.Map<List<displayGradeDTO>>(grades);
                    return Ok(grdDTO);
                }
            }
        }

        [HttpGet("ByStudentNationalid/{studentNationalId}")]
        public IActionResult getGradesBtStdNationalId(string studentNationalId)
        {
            var std = unit.StudentRepo.getParentByNtionalId(studentNationalId);
            if (std == null) { return NotFound("Invalid National Id"); }
            else
            {
                var grades = unit.GradeRepo.getByStdNationalId(studentNationalId);
                if (grades == null) { return NotFound(); }
                else
                {
                    List<displayGradeDTO> grdDTO = mapper.Map<List<displayGradeDTO>>(grades);
                    return Ok(grdDTO);
                }
            }
        }


    }
}
