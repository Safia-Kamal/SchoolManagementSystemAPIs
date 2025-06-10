using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.DTOs.TeacherClassSubjectDTOs;
using School.Models;
using School.UnitOfWorks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherClassSubjectController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public TeacherClassSubjectController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        

        [HttpGet]
        public IActionResult getAll()
        {
            var teachClsSub = unit.TeacherClassSubjectRepo.GetAllWithObjs();
            if (teachClsSub == null) { 
                return NotFound("there is no Classes Subjects to any teacher");
            }
            else
            {
                List<displayTeacherClassSubjectDTO> tcs = mapper.Map<List<displayTeacherClassSubjectDTO>>(teachClsSub);
                return Ok(tcs);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult getById(int id) { 
            var teachClsSub = unit.TeacherClassSubjectRepo.getByIdWithObjs(id);
            if (teachClsSub == null) {
                return NotFound("No Teacher Class Subjects with this Id");
            }
            else
            {
                var tcsDto = mapper.Map<displayTeacherClassSubjectDTO>(teachClsSub);
                return Ok(tcsDto);
            }
        }

        [HttpGet("ByTeacherId/{teachId:int}")]
        public IActionResult getByTeachId(int teachId)
        {
            var tch = unit.TeacherRepo.getById(teachId);
            if (tch == null) {
                return NotFound("No Teacher With This Id");
            }else
             {
                var teachClsSub = unit.TeacherClassSubjectRepo.getByTeachId(teachId);
                if (teachClsSub == null)
                {
                    return NotFound("No Teacher Class Subjects with this Id");
                }
                else
                {
                    var tcsDto = mapper.Map<List<displayTeacherClassSubjectDTO>>(teachClsSub);
                    return Ok(tcsDto);
                }


             }
        }

        [HttpGet("ByClassId/{clsId:int}")]
        public IActionResult getByClsId(int clsId)
        {
            var cls = unit.ClassRepo.getById(clsId);
            if (cls == null) {
                return NotFound("No Classes With This Id");
            }
            else {
                var teachClsSub = unit.TeacherClassSubjectRepo.getByClsId(clsId);
                if (teachClsSub == null)
                {
                    return NotFound("No Teacher Class Subjects with this Id");
                }
                else
                {
                    var tcsDto = mapper.Map<List<displayTeacherClassSubjectDTO>>(teachClsSub);
                    return Ok(tcsDto);
                }
            }
        }

        [HttpGet("BySubId/{subId:int}")]
        public IActionResult getBySubId(int subId)
        {
            var sub = unit.SubjectRepo.getById(subId);
            if (sub == null)
            {
                return NotFound("No Subject With This Id");
            }
            else {

                var teachClsSub = unit.TeacherClassSubjectRepo.getBySubId(subId);
                if (teachClsSub == null)
                {
                    return NotFound("No Teacher Class Subjects with this Id");
                }
                else
                {
                    var tcsDto = mapper.Map<List<displayTeacherClassSubjectDTO>>(teachClsSub);
                    return Ok(tcsDto);
                }
            }
        }


        [HttpGet("ByTeacherName/{teachName:alpha}")]
        public IActionResult getByTeachName(string teachName)
        {
            var tch = unit.TeacherRepo.getByName(teachName);
            if (tch == null)
            {
                return NotFound("No Teacher With This name");
            }
            else
            {

                var teachClsSub = unit.TeacherClassSubjectRepo.getByTeachName(teachName);
                if (teachClsSub == null)
                {
                    return NotFound("No Teacher Class Subjects with this Id");
                }
                else
                {
                    var tcsDto = mapper.Map<List<displayTeacherClassSubjectDTO>>(teachClsSub);
                    return Ok(tcsDto);
                }
            }
        }

        [HttpGet("ByClassName/{ClsName:alpha}")]
        public IActionResult getByClsName(string clsName)
        {
            var cls = unit.ClassRepo.getClassByName(clsName);
            if (cls == null)
            {
                return NotFound("No Classes With This Name");
            }
            else
            {
                var teachClsSub = unit.TeacherClassSubjectRepo.getByClsName(clsName);
                if (teachClsSub == null)
                {
                    return NotFound("No Teacher Class Subjects with this Id");
                }
                else
                {
                    var tcsDto = mapper.Map<List<displayTeacherClassSubjectDTO>>(teachClsSub);
                    return Ok(tcsDto);
                }
            }
        }

        [HttpGet("BySubName/{name:alpha}")]
        public IActionResult getBySubNamw(string subName)
        {
            var sub = unit.SubjectRepo.getSubByName(subName);
            if (sub == null)
            {
                return NotFound("No Classes With This Name");
            }
            else
            {
                var teachClsSub = unit.TeacherClassSubjectRepo.getBySubName(subName);
                if (teachClsSub == null)
                {
                    return NotFound("No Teacher Class Subjects with this Id");
                }
                else
                {
                    var tcsDto = mapper.Map<List<displayTeacherClassSubjectDTO>>(teachClsSub);
                    return Ok(tcsDto);
                }
            }
        }

        [HttpPost]
        public IActionResult addTeachClsSub(addTeacherClassSubjectDTO teachClsSubDto)
        {
            TeacherClassSubject teachClsSub = mapper.Map<TeacherClassSubject>(teachClsSubDto);
            unit.TeacherClassSubjectRepo.add(teachClsSub);
            displayTeacherClassSubjectDTO disTeachClsSub = mapper.Map<displayTeacherClassSubjectDTO>(teachClsSub);
            return Ok(disTeachClsSub);
        }

        [HttpPut("{id:int}")]
        public IActionResult editTeacherClassSub(int id , addTeacherClassSubjectDTO teachClsSub)
        {
            var tcs = unit.TeacherClassSubjectRepo.getByIdWithObjs(id);
            if(tcs == null)
            {
                return NotFound("There is no Teacher Class Subject With this Id");
            }
            else
            {
                mapper.Map(teachClsSub, tcs);
                unit.TeacherClassSubjectRepo.edit(tcs);
                unit.save();
                displayTeacherClassSubjectDTO disTCS = mapper.Map<displayTeacherClassSubjectDTO>
                    (unit.TeacherClassSubjectRepo.getByIdWithObjs(id));
                return Ok(disTCS);

            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult deleteTeachClsSub(int id) {
            var tcs = unit.TeacherClassSubjectRepo.getByIdWithObjs(id);
            if(tcs == null)
            {
                return NotFound("There is no Teacher Class Subject With this Id");
            }
            else
            {
                displayTeacherClassSubjectDTO disTCS = mapper.Map<displayTeacherClassSubjectDTO>(tcs);
                unit.TeacherClassSubjectRepo.delete(id);
                unit.save();
                return Ok(disTCS);
            }

        }


    }
}
