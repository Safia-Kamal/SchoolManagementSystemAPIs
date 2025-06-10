using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.DTOs.ClassSubjectDTOs;
using School.Models;
using School.UnitOfWorks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassSubjectController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public ClassSubjectController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult getAll()
        {
            var ClsSubs = unit.ClassSubjectsRepo.getAllWithObjs();
            if (ClsSubs == null) { 
                return NotFound("There is no Class Subjects");
            }
            else
            {
                List<displayClassSubjectDTO> clsSubDTO = mapper.Map<List<displayClassSubjectDTO>>(ClsSubs);
                return Ok(clsSubDTO);
            }
        }

        [HttpGet("{Id:int}")]
        public IActionResult getById(int Id)
        {
            var ClsSubs = unit.ClassSubjectsRepo.getByIdWithObjs(Id);
            if (ClsSubs == null)
            {
                return NotFound("There is no Class Subjects With This Id");
            }
            else
            {
                List<displayClassSubjectDTO> clsSubDTO = mapper.Map<List<displayClassSubjectDTO>>(ClsSubs);
                return Ok(clsSubDTO);
            }
        }

        [HttpGet("ByClassId/{ClsId}")]
        public IActionResult getByClsId(int ClsId) {
            var cls = unit.ClassRepo.getById(ClsId);
            if (cls == null)
            {
                return NotFound("no Classes With This Id");
            }
            else
            {
                var ClsSubs = unit.ClassSubjectsRepo.getAllByClsId(ClsId);
                if (ClsSubs == null)
                {
                    return NotFound("There is no Class Subjects");
                }
                else
                {
                    List<displayClassSubjectDTO> clsSubDTO = mapper.Map<List<displayClassSubjectDTO>>(ClsSubs);
                    return Ok(clsSubDTO);
                }
            }

        }

        [HttpGet("BySubjectId/{SubId}")]
        public IActionResult getBySubId(int SubId)
        {
            var sub = unit.SubjectRepo.getById(SubId);
            if (sub == null)
            {
                return NotFound("no Subjects With This Id");
            }
            else
            {
                var ClsSubs = unit.ClassSubjectsRepo.getAllBySubId(SubId);
                if (ClsSubs == null)
                {
                    return NotFound("There is no Class Subjects");
                }
                else
                {
                    List<displayClassSubjectDTO> clsSubDTO = mapper.Map<List<displayClassSubjectDTO>>(ClsSubs);
                    return Ok(clsSubDTO);
                }
            }
        }

        [HttpGet("ByClassName/{ClsName}")]
        public IActionResult getByClsName(string ClsName)
        {
            var sub = unit.ClassRepo.getClassByName(ClsName);
            if (sub == null)
            {
                return NotFound("no Classes With This Name");
            }
            else
            {
                var ClsSubs = unit.ClassSubjectsRepo.getAllByClsName(ClsName);
                if (ClsSubs == null)
                {
                    return NotFound("There is no Class Subjects");
                }
                else
                {
                    List<displayClassSubjectDTO> clsSubDTO = mapper.Map<List<displayClassSubjectDTO>>(ClsSubs);
                    return Ok(clsSubDTO);
                }
            }
        }

        [HttpGet("BySubjectName/{SubName}")]
        public IActionResult getBySubName(string SubName)
        {
            var sub = unit.SubjectRepo.getSubByName(SubName);
            if (sub == null)
            {
                return NotFound("no Subjects With This Name");
            }
            else
            {
                var ClsSubs = unit.ClassSubjectsRepo.getAllBySubName(SubName);
                if (ClsSubs == null)
                {
                    return NotFound("There is no Class Subjects");
                }
                else
                {
                    List<displayClassSubjectDTO> clsSubDTO = mapper.Map<List<displayClassSubjectDTO>>(ClsSubs);
                    return Ok(clsSubDTO);
                }
            }
        }

        [HttpPost]
        public IActionResult addClassSubject(addClassSubjectDTO classSubjectDTO)
        {
            ClassSubject clsSub = mapper.Map<ClassSubject>(classSubjectDTO);
            unit.ClassSubjectsRepo.add(clsSub);
            unit.save();
            displayClassSubjectDTO clsSubDTO = mapper.Map<displayClassSubjectDTO>
                (unit.ClassSubjectsRepo.getByIdWithObjs(clsSub.Id));
            return Ok(clsSubDTO);
        }

        [HttpPut]
        public IActionResult editClassSub(int id , addClassSubjectDTO classSubjectDTO)
        {
            var clsSub = unit.ClassSubjectsRepo.getByIdWithObjs(id);
            if(clsSub == null)
            {
                return NotFound("No Class Subject With this Id");
            }
            else
            {
                mapper.Map(classSubjectDTO, clsSub);
                unit.ClassSubjectsRepo.add(clsSub);
                unit.save();
                displayClassSubjectDTO clsSubDto = mapper.Map<displayClassSubjectDTO>
                    (unit.ClassSubjectsRepo.getByIdWithObjs(id));
                return Ok(clsSubDto);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult deleteClassSubject(int id) {
            var clsSub = unit.ClassSubjectsRepo.getByIdWithObjs(id);
            if( clsSub == null)
            {
                return NotFound("No Class Subject With This Id");
            }
            else
            {
                displayClassSubjectDTO clsSubDto = mapper.Map<displayClassSubjectDTO>(clsSub);
                unit.ClassSubjectsRepo.delete(id);
                unit.save();
                return Ok(clsSubDto);
            }
        }

    }
}
