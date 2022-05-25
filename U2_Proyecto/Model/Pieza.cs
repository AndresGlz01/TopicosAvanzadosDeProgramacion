using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_Proyecto.Model
{
    internal class Pieza
    {
        public string Titulo { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
        public string? FechaCreacion { get; set; }
        public string? Descripcion { get; set; }
        public Artista? Artista { get; set; }
    }
}
