using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GFT2.NETDIO_CatalagoDeBarbearias.Model
{
    public class BarbeariaModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da barbearia deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do(a) dono(a) deve conter entre 3 e 100 caracteres")]
        public string Dono { get; set; }
        public double ValorMensalidade { get; set; }
        public int MesesContrato { get; set; }
    }
}
