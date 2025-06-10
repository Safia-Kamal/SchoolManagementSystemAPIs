using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.DTOs.ParentDTOs;
using School.Models;
using School.UnitOfWorks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;

        public ParentController(UnitOfWork unit , IMapper mapper) {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult getAll() {
            var parents = unit.ParentRepo.getAll();
            if (parents == null) { 
                return NotFound("No Parents Yet");
            }
            else
            {
                List<displayParentDTO> parDTo = mapper.Map<List<displayParentDTO>>(parents);
                return Ok(parDTo);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult getById(int id) { 
            var parent = unit.ParentRepo.getById(id);
            if (parent == null) {
                return NotFound("No Parents With This Id");
            }
            else
            {
                displayParentDTO parDto = mapper.Map<displayParentDTO>(parent);
                return Ok(parDto);
            }
        }

        [HttpPost]
        public IActionResult addParent(addParentDTO parDto) { 
            Parent par = mapper.Map<Parent>(parDto);
            unit.ParentRepo.add(par);
            unit.save();
            displayParentDTO parDTO = mapper.Map<displayParentDTO>(par);
            return Ok(parDTO);

        }


        [HttpPut]
        public IActionResult editParent(int id , addParentDTO addParentDTO) { 
            var par = unit.ParentRepo.getById(id);
            if (par == null)
            {
                return NotFound("No Parents with This this Id");
            }
            else
            {
                mapper.Map(addParentDTO, par);
                unit.parentRepo.edit(par);
                unit.save();
                displayParentDTO parDTO = mapper.Map<displayParentDTO>(par);
                return Ok(parDTO);
            }
        }

        [HttpDelete]
        public IActionResult deleteParent(int id) { 
            var parent = unit.ParentRepo.getById(id);
            if(parent == null)
            {
                return NotFound("No Parents With This Id");
            }
            else
            {
                displayParentDTO parDto = mapper.Map<displayParentDTO>(parent);
                unit.ParentRepo.delete(id);
                unit.save();
                return Ok(parDto);
            }
        }
    }
}
