using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT2.NETDIO_CatalagoDeBarbearias.Exceptions
{
    public class BarbeariaJaCadastradaException : Exception
    {
        public BarbeariaJaCadastradaException()
            : base("Esta barbearia já está cadastrada")
        { }
    }
}
