
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT2.NETDIO_CatalagoDeBarbearias.Entity
{
    public class Barbearia
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Dono { get; set; }
        public double ValorMensalidade { get; set; }
        public int MesesContrato { get; set; }
    }
}
