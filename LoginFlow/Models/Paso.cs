using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginFlow.Models
{
    public class Paso
    {
        public int id { get; set; }
        public string paso { get; set; }
        public string status { get; set; }
        public int idReceta { get; set; }
    }
}
