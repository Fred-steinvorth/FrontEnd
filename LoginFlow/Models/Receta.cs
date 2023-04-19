using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginFlow.Models
{
    public class Receta
    {
        public int id { get; set; }
        public string nombreReceta { get; set; }
        public int? calificacion { get; set; }
        public int? status { get; set; }
        public Usuario usuario { get; set; }
        public List<Ingrediente> ingredientes { get; set; }
        public List<Paso> pasos { get; set; }

        public Receta()
        {
            this.usuario = new Usuario();
            this.ingredientes = new List<Ingrediente>();
            this.pasos = new List<Paso>();
        }
    }
}
