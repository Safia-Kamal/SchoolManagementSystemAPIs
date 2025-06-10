using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.DTOs.SubjectDTOs;
using School.Models;
using School.UnitOfWorks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public SubjectController(UnitOfWork unit , IMapper mapper) {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult getAllSubjects()
        {
            var subjects = unit.SubjectRepo.getAll();
            if(subjects == null)
            {
                return NotFound();
            }
            else
            {
                List<displaySubjectDTO> subs = mapper.Map<List<displaySubjectDTO>>(subjects);
                return Ok(subs);
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult getSubjectById(int id) { 
            var subject = unit.SubjectRepo.getById(id);
            if (subject == null)
            {
                return NotFound();
            }
            else { 
                displaySubjectDTO subDTO = mapper.Map<displaySubjectDTO>(subject);
                return Ok(subDTO);
            }
        }


        [HttpGet("{name}")]
        public IActionResult getSubjectByName(string name) {
            var sub = unit.SubjectRepo.getSubByName(name);
            if(sub == null)
            {
                return NotFound();
            }
            else
            {
                displaySubjectDTO subDTO = mapper.Map<displaySubjectDTO>(sub);
                return Ok(subDTO);
            }
        }

        [HttpPost]
        public IActionResult addSubject(addSubjectDTO sub)
        {
            Subject subject = mapper.Map<Subject>(sub);
            unit.SubjectRepo.add(subject);
            unit.save();
            displaySubjectDTO subDTO = mapper.Map<displaySubjectDTO>(subject);
            return Ok(subDTO);


        }

        [HttpPut("{id}")]
        public IActionResult editSubject(int id,addSubjectDTO sub)
        {
            var subject = unit.SubjectRepo.getById(id);
            if(subject == null)
            {
                return NotFound();
            }
            else
            {
                mapper.Map(sub , subject);
                unit.SubjectRepo.edit(subject);
                unit.save();
                displaySubjectDTO subDTO = mapper.Map<displaySubjectDTO>(subject);
                return Ok(subDTO);
            }
        }

        [HttpDelete]
        public IActionResult deleteSubject(int id) {
            var subject = unit.SubjectRepo.getById(id);
            if( subject == null)
            {
                return NotFound();
            }
            else
            {
                displaySubjectDTO subDTO = mapper.Map<displaySubjectDTO>(subject);
                unit.SubjectRepo.delete(id);
                unit.save();
                return Ok(subDTO);
            }
        }




    }
}
