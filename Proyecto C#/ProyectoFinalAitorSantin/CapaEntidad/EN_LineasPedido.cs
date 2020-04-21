using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EN_LineasPedido
    {
        public string codpedido { get; set; }
        public int nlinea { get; set; }
        public string codarticulo { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
    }
}
