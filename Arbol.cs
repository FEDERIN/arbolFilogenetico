using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arbolFilogenetico
{
    class Arbol
    {
        public String id;
        public String descripcion;
        public String padre;
        public int nivel;
        public List<Arbol> arbols;
    }
}
