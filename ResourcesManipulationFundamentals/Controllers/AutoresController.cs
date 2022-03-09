using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResourcesManipulationFundamentals.Entities;
using ResourcesManipulationFundamentals.Context;
using ResourcesManipulationFundamentals.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace ResourcesManipulationFundamentals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper _mapper;

        public AutoresController(AppDbContext context,IMapper mapper)
        {
            this.context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AutorDTO>> Get()
        {
            var autores = context.Autores.Include(x => x.Libros).ToList();
            var autoresDTO = _mapper.Map<List<AutorDTO>>(autores);
            return autoresDTO;
        }

        [HttpGet("{id}", Name = "ObtenerAutor")]
        public ActionResult<AutorDTO> Get(int id)
        {
            var autor = context.Autores.Include(x => x.Libros).FirstOrDefault(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }
            //var autorDTO = _mapper.Map<AutorDTO>(autor);
            return _mapper.Map<AutorDTO>(autor);
            //return new AutorDTO
            //{
            //    Id = autor.Id,
            //    Nombre = autor.Nombre
            //};
            //return autor;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InsertAutorDTO insertAutor)
        {
            // Esto no es necesario en asp.net core 2.1 en adelante
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            var autor = _mapper.Map<Autor>(insertAutor);
            context.Autores.Add(autor);
            await context.SaveChangesAsync();
            var autorDTO = _mapper.Map<AutorDTO>(autor);
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autorDTO);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] InsertAutorDTO updateAutorDTO)
        {
            var autor = _mapper.Map<Autor>(updateAutorDTO);
            autor.Id = id;
            // Esto no es necesario en asp.net core 2.1
            // if (ModelState.IsValid){

            // }

            //if (id != value.Id)
            //{
            //    return BadRequest();
            //}

            context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id,[FromBody] JsonPatchDocument<InsertAutorDTO> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if (autor == null)
                return NotFound();

            var autorDTO = _mapper.Map<InsertAutorDTO>(autor);

            patchDocument.ApplyTo(autorDTO, ModelState);
            
            _mapper.Map(autorDTO, autor);
            
            var isValid = TryValidateModel(autor);

            if (!isValid)
                return BadRequest(ModelState);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Autor>> Delete(int id)
        {
            var autorId = context.Autores.Select(x=>x.Id).FirstOrDefault(x => x == id);

            if (autorId == default(int))
            {
                return NotFound();
            }

            context.Autores.Remove(new Autor { Id = autorId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}