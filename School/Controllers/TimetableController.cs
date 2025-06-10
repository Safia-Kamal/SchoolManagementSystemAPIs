using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.DTOs.TimetableDTOs;
using School.Models;
using School.UnitOfWorks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;

        public TimetableController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }


        [HttpGet]
        public IActionResult getAllTimetables()
        {
            var Timetables = unit.TimetableRepo.getAllWithSubAndTeacher();
            if (Timetables == null)
            {
                return NotFound();
            }
            else
            {
                List<displayTimetableDTO> T = mapper.Map<List<displayTimetableDTO>>(Timetables);
                return Ok(T);
            }
        }

        [HttpGet("{id}")]
        public IActionResult getTimetableById(int id)
        {
            var timetable = unit.TimetableRepo.getByIdWithSubAndTeacher(id);
            if (timetable == null)
            {
                return NotFound("No Timetables with this Id");
            }
            else
            {
                displayTimetableDTO timeDTO = mapper.Map<displayTimetableDTO>(timetable);
                return Ok(timeDTO);
            }
        }


        [HttpPost]
        public IActionResult addTimttable(addTimetableDTO timetableDTO)
        {
            // Parse StartTime
            if (!DateTime.TryParse(timetableDTO.StartTime, out DateTime parsedStart))
            {
                return BadRequest("Invalid StartTime format. Please use formats like '2 PM' or '3:15 PM'.");
            }

            // Parse EndTime
            if (!DateTime.TryParse(timetableDTO.EndTime, out DateTime parsedEnd))
            {
                return BadRequest("Invalid EndTime format. Please use formats like '2 PM' or '3:15 PM'.");
            }

            // Manually map the DTO to the Timetable entity
            Timetable timetable = new Timetable
            {
                ClassId = timetableDTO.ClassId,
                SubjectId = timetableDTO.SubjectId,
                TeacherId = timetableDTO.TeacherId,
                DayOfWeek = timetableDTO.DayOfWeek,
                StartTime = parsedStart.TimeOfDay,
                EndTime = parsedEnd.TimeOfDay
            };

            unit.TimetableRepo.add(timetable);
            unit.save();

            displayTimetableDTO timeDTO = mapper.Map<displayTimetableDTO>(
                unit.TimetableRepo.getByIdWithSubAndTeacher(timetable.Id)
            );

            return Ok(timeDTO);
        }


        [HttpPut("{id}")]
        public IActionResult editTimetable(int id, addTimetableDTO timetableDTO)
        {
            var timetable = unit.TimetableRepo.getByIdWithSubAndTeacher(id);
            if (timetable == null)
            {
                return NotFound();
            }
            else
            {
                // Parse StartTime
                if (!DateTime.TryParse(timetableDTO.StartTime, out DateTime parsedStart))
                {
                    return BadRequest("Invalid StartTime format. Please use formats like '2 PM' or '3:15 PM'.");
                }

                // Parse EndTime
                if (!DateTime.TryParse(timetableDTO.EndTime, out DateTime parsedEnd))
                {
                    return BadRequest("Invalid EndTime format. Please use formats like '2 PM' or '3:15 PM'.");
                }


                timetable.ClassId = timetableDTO.ClassId;
                timetable.SubjectId = timetableDTO.SubjectId;
                timetable.TeacherId = timetableDTO.TeacherId;
                timetable.DayOfWeek = timetableDTO.DayOfWeek;
                timetable.StartTime = parsedStart.TimeOfDay;
                timetable.EndTime = parsedEnd.TimeOfDay;

                unit.TimetableRepo.edit(timetable);
                unit.save();

                displayTimetableDTO timeDTO = mapper.Map<displayTimetableDTO>(
                    unit.TimetableRepo.getByIdWithSubAndTeacher(timetable.Id)
                );

                return Ok(timeDTO);

            }
        }

        [HttpGet("ByTeacherId/{TeacherId:int}")]
        public IActionResult getTimetablesByTeacherId(int TeacherId)
        {
            var teacher = unit.TeacherRepo.getById(TeacherId);
            if (teacher == null) { return NotFound("Invalid Teacher Id"); }
            else
            {
                var timetables = unit.TimetableRepo.getTimetablesByTeacherId(TeacherId);
                if (timetables == null)
                {
                    return NotFound();
                }
                else
                {
                    List<displayTimetableDTO> timetableDTO = mapper.Map<List<displayTimetableDTO>>(timetables);
                    return Ok(timetableDTO);
                }
            }
        }

        [HttpGet("ByClassId/{ClassId:int}")]
        public IActionResult getTimetablesByClassId(int ClassId)
        {
            var cls = unit.ClassRepo.getById(ClassId);
            if (cls == null) { return NotFound("Invalid Class Id"); }
            else
            {
                var timetables = unit.TimetableRepo.getTimetablesByClassId(ClassId);
                if (timetables == null)
                {
                    return NotFound();
                }
                else
                {
                    List<displayTimetableDTO> timetableDTO = mapper.Map<List<displayTimetableDTO>>(timetables);
                    return Ok(timetableDTO);
                }
            }
        }

        [HttpGet("ByTeacherName/{TeacherName}")]
        public IActionResult getTimetablesByTeacherName(string TeacherName)
        {
            var teacher = unit.TeacherRepo.getByName(TeacherName);
            if (teacher == null) { return NotFound("Invalid Teacher Name"); }
            else
            {
                var timetables = unit.TimetableRepo.getTimetablesByTeacherName(TeacherName);
                if (timetables == null)
                {
                    return NotFound();
                }
                else
                {
                    List<displayTimetableDTO> timetableDTO = mapper.Map<List<displayTimetableDTO>>(timetables);
                    return Ok(timetableDTO);
                }
            }
        }

        [HttpGet("ByClassName/{ClassName}")]
        public IActionResult getTimetablesByClassName(string ClassName)
        {
            var cls = unit.ClassRepo.getClassByName(ClassName);
            if (cls == null) { return NotFound("Invalid Class Name"); }
            else
            {
                var timetables = unit.TimetableRepo.getTimetablesByClassName(ClassName);
                if (timetables == null)
                {
                    return NotFound();
                }
                else
                {
                    List<displayTimetableDTO> timetableDTO = mapper.Map<List<displayTimetableDTO>>(timetables);
                    return Ok(timetableDTO);
                }
            }
        }

        [HttpDelete]
        public IActionResult deleteTimetable(int id)
        {
            var timetable = unit.TimetableRepo.getByIdWithSubAndTeacher(id);
            if(timetable == null)
            {
                return NotFound();
            }
            else
            {
                displayTimetableDTO timeDto = mapper.Map<displayTimetableDTO>(timetable);
                unit.TimetableRepo.delete(id);
                unit.save();
                return Ok(timeDto);

            }

        }





    }
}
