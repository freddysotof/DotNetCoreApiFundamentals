using NetCoreFundamentals.Context;
using NetCoreFundamentals.Entities;
using NetCoreFundamentals.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreFundamentals.Helpers;

namespace NetCoreFundamentals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IClaseB claseB;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(AppDbContext context, ClaseB claseB,ILogger<AutoresController> logger)
        {
            this.context = context;
            this.claseB = claseB;
            this.logger = logger;
        }

        // GET api/autores
        [HttpGet("/listado")]
        [HttpGet("listado")]
        [HttpGet]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            throw new NotImplementedException();
            logger.LogInformation("Obteniendo los autores");
            //throw new NotImplementedException();
            claseB.HacerAlgo();
            return context.Autores.ToList();
        }

        [HttpGet("Primer")]
        public ActionResult<Autor> GetPrimerAutor()
        {
            return context.Autores.FirstOrDefault();
        }

        // GET api/autores/5 
        // GET api/autores/5/felipe
        [HttpGet("{id}/{param2=Gavilan}")]
        public async Task<ActionResult<Autor>> Get(int id, string param2)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                logger.LogWarning($"El autor de {id} no ha sido encontrado");
                return NotFound();
            }

            return autor;
        }

        // POST api/autores
        [HttpPost]
        public ActionResult Post([FromBody] Autor autor)
        {
            context.Add(autor);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autor);
        }

        // PUT api/autores/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Autor author)
        {
            context.Entry(author).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        // DELETE api/autores/5
        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = context.Autores.FirstOrDefault(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            context.Remove(autor);
            context.SaveChanges();
            return Ok(autor);
        }

    }
}