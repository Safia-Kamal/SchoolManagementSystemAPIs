using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.DTOs.ClassDTOs;
using School.Models;
using School.Repository;
using School.UnitOfWorks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        UnitOfWork unit;
        public IMapper mapper;
        public ClassController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAllClasses()
        {
            var classes = unit.ClassRepo.getAll();
            if (classes == null)
            {
                return NotFound();
            }
            else
            {
                List<DisplayClassDTO> res = new List<DisplayClassDTO>();
                foreach (var cls in classes)
                {
                    DisplayClassDTO dto = mapper.Map<DisplayClassDTO>(cls);
                    res.Add(dto);

                }
                return Ok(res);
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult getClassById(int id)
        {
            var c = unit.ClassRepo.getById(id);
            if (c == null)
                return NotFound();
            var result = mapper.Map<DisplayClassDTO>(c);

            return Ok(result);
        }


        [HttpPost]
        public IActionResult addClass(AddClassDTO c)
        {
            Class cl = mapper.Map<Class>(c);
            unit.ClassRepo.add(cl);
            unit.save();
            DisplayClassDTO cdto = mapper.Map<DisplayClassDTO>(cl);
            return Ok(cdto);
        }


        [HttpPut("{id}")]
        public IActionResult editClass(int id, AddClassDTO cl)
        {
            var existingClass = unit.ClassRepo.getById(id);
            if (existingClass == null)
            {
                return NotFound();
            }
            
            mapper.Map(cl, existingClass);
            unit.ClassRepo.edit(existingClass);
            unit.save();
            DisplayClassDTO cdto = mapper.Map<DisplayClassDTO>(existingClass);
            return Ok(cdto); 
        }


        [HttpDelete]
        public IActionResult deleteClass(int id)
        {
            var c = unit.ClassRepo.getById(id);
            if (c == null)
            {
                return NotFound();
            }
            else
            {
                DisplayClassDTO cdto = mapper.Map<DisplayClassDTO>(c);
                unit.ClassRepo.delete(id);
                unit.save();
                return Ok(cdto);
            }

        }

        [HttpGet("{name}")]
        public IActionResult getClassByName(string name) {

            var c = unit.ClassRepo.getClassByName(name);
            if(c == null)
            {
                return NotFound();
            }
            else
            {
                DisplayClassDTO cdto = mapper.Map<DisplayClassDTO>(c);
                return Ok(cdto);
            }

        }





    }
}
