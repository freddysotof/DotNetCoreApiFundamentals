using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesManipulationFundamentals.Models
{
    public class InsertAutorDTO
    {
        [Required]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Identificacion { get; set; }
        //public List<Libro> Libros { get; set; }
    }
}
