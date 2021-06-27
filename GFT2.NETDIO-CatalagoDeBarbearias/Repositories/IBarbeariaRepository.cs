using GFT2.NETDIO_CatalagoDeBarbearias.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT2.NETDIO_CatalagoDeBarbearias.Repositories
{
    public interface IBarbeariaRepository : IDisposable
    {
        Task<List<Barbearia>> Obter(int pagina, int quantidade);
        Task<Barbearia> Obter(Guid id);
        Task<List<Barbearia>> Obter(string nome, string dono);
        Task Inserir(Barbearia barbearia);
        Task Atualizar(Barbearia barbearia);
        Task Remover(Guid id);
        

    }
}
