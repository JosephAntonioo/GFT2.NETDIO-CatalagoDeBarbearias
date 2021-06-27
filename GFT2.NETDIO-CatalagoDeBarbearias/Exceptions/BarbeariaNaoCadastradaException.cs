using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT2.NETDIO_CatalagoDeBarbearias.Exceptions
{
    public class BarbeariaNaoCadastradaException : Exception
    {
        public BarbeariaNaoCadastradaException()
            : base("Esta barbearia não está cadastrada")
        { }
    }
}
