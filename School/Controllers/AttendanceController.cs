using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.DTOs.AttendanceDTO;
using School.DTOs.AttendanceDTOs;
using School.Models;
using School.UnitOfWorks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public AttendanceController(UnitOfWork unit , IMapper mapper) { 
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult getAll()
        {
            var att = unit.AttendanceRepo.getAllWithStd();
            if (att == null) { 
                return NotFound();
            }
            List<displayAttendanceDTO> attDTO = mapper.Map<List<displayAttendanceDTO>>(att);
            return Ok(attDTO);

        }

        [HttpGet("{id}")]
        public IActionResult getById(int id)
        {
            var attendance = unit.AttendanceRepo.getByIdWithStd(id);
            if(attendance == null)
            {
                return NotFound();
            }
            else
            {
                displayAttendanceDTO attDTO = mapper.Map<displayAttendanceDTO>(attendance);
                return Ok(attDTO);
            }
        }




        //[HttpPost]
        //public IActionResult setAttendance(AddAttendanceDTO attendance, int stdId)
        //{
        //    var s = unit.StudentRepo.getById(stdId);
        //    if (s == null)
        //    {
        //        return NotFound("invalid Student Id");
        //    }
        //    else
        //    {
        //        Attendance att = mapper.Map<Attendance>(attendance);
        //        unit.AttendanceRepo.add(att);
        //        unit.save();
        //        displayAttendanceDTO attDTO = mapper.Map<displayAttendanceDTO>(att);
        //        return Ok(attDTO);
        //    }
        //}


        [HttpPost]
        public IActionResult AddAttendance([FromBody] AddAttendanceDTO attendanceDto, int stdId)
        {
            if (attendanceDto == null)
                return BadRequest("No attendance data sent!");

            var student = unit.StudentRepo.getById(attendanceDto.StudentId);
            if (student == null)
                return NotFound("Invalid Student Id");

            
            var existing = unit.AttendanceRepo.check(attendanceDto.StudentId);
            if (existing != null)
                return BadRequest("Attendance already recorded for today.");

            
            Attendance att = mapper.Map<Attendance>(attendanceDto);
            unit.AttendanceRepo.add(att);
            unit.save();

            
            var savedAttendance = unit.AttendanceRepo.getByIdWithStd(att.Id);
            if (savedAttendance == null || savedAttendance.Student == null)
                return StatusCode(500, "Attendance saved but student info is missing.");

           
            displayAttendanceDTO attDTO = mapper.Map<displayAttendanceDTO>(savedAttendance);
            return Ok(attDTO);
        }



        [HttpGet("ByStudentName/{studentName}")]
        public IActionResult getAttendanceByStudentName(string studentName) { 
            var std = unit.StudentRepo.getByName(studentName);
            if (std == null) { 
                return NotFound("Invalid Student Name");
            }
            else
            {
                var att = unit.AttendanceRepo.getByStudentName(studentName);
                if (att == null) { return NotFound(); }
                else
                {
                    List<displayAttendanceDTO> attDto = mapper.Map<List<displayAttendanceDTO>>(att);
                    return Ok(attDto);
                }
            }
        }


        [HttpGet("ByStudentId/{studentId}")]
        public IActionResult getAttendanceByStudentName(int studentId)
        {
            var std = unit.StudentRepo.getById(studentId);
            if (std == null)
            {
                return NotFound("Invalid Student Name");
            }
            else
            {
                var att = unit.AttendanceRepo.getByStudentId(studentId);
                if (att == null) { return NotFound(); }
                else
                {
                    List<displayAttendanceDTO> attDto = mapper.Map<List<displayAttendanceDTO>>(att);
                    return Ok(attDto);
                }
            }
        }


        [HttpGet("ByClassName/{className}")]
        public IActionResult getAttendanceByClassName(string className)
        {
            var std = unit.ClassRepo.getClassByName(className);
            if (std == null)
            {
                return NotFound("Invalid class name");
            }
            else
            {
                var att = unit.AttendanceRepo.getByClassName(className);
                if (att == null) { return NotFound(); }
                else
                {
                    List<displayAttendanceDTO> attDto = mapper.Map<List<displayAttendanceDTO>>(att);
                    return Ok(attDto);
                }
            }
        }


        [HttpGet("ByClassId/{classId}")]
        public IActionResult getAttendanceByClassName(int classId)
        {
            var cls = unit.ClassRepo.getById(classId);
            if (cls == null)
            {
                return NotFound("Invalid class Id");
            }
            else
            {
                var att = unit.AttendanceRepo.getByClassId(classId);
                if (att == null) { return NotFound(); }
                else
                {
                    List<displayAttendanceDTO> attDto = mapper.Map<List<displayAttendanceDTO>>(att);
                    return Ok(attDto);
                }
            }
        }

        [HttpGet("bydate/{date:datetime}")]
        public IActionResult getAttendanceByDate(DateTime date) { 
            var att = unit.AttendanceRepo.getByDate(date);
            if (att == null) { 
                return NotFound();
            }
            else
            {
                List<displayAttendanceDTO> attDTO = mapper.Map<List<displayAttendanceDTO>>(att);
                return Ok(attDTO);
            }
        }


        [HttpDelete]
        public IActionResult deleteAttendance(int id) { 
            var att = unit.AttendanceRepo.getByIdWithStd(id);
            if (att == null) { 
                return NotFound();
            }
            else
            {
                displayAttendanceDTO attDTO = mapper.Map<displayAttendanceDTO>(att);
                unit.AttendanceRepo.delete(id);
                unit.save();
                return Ok(attDTO);

            }
        }

        [HttpPut("{id}")]
        public IActionResult editAttendance(int id, AddAttendanceDTO att)
        {
            var attendance = unit.AttendanceRepo.getByIdWithStd(id);
            if (attendance == null)
            {
                return NotFound();
            }
            else
            {
                mapper.Map(att , attendance);
                unit.AttendanceRepo.edit(attendance);
                unit.save();
                displayAttendanceDTO attDto =  mapper.Map<displayAttendanceDTO>(attendance);
                return Ok(attDto);

            }
        }




        [HttpGet("TodayByClassName/{className}")]
        public IActionResult GetTodayAttendanceByClass(string className)
        {
            var cls = unit.ClassRepo.getClassByName(className);
            if (cls == null)
                return NotFound("Invalid class name");

            var attList = unit.AttendanceRepo.getTodayByClassName(className);

            if (attList == null || !attList.Any())
                return Ok(new List<displayAttendanceDTO>());

            var attDto = mapper.Map<List<displayAttendanceDTO>>(attList);
            return Ok(attDto);
        }

    }
}
