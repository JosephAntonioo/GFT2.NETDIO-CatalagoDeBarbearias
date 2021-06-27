using GFT2.NETDIO_CatalagoDeBarbearias.Model;
using GFT2.NETDIO_CatalagoDeBarbearias.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT2.NETDIO_CatalagoDeBarbearias.Service
{
    public interface IBarbeariaService
    {
        Task<List<BarbeariaViewModel>> Obter(int pagina, int quantidade);
        Task<BarbeariaViewModel> Obter(Guid idBarbearia);
        Task<BarbeariaViewModel> Inserir(BarbeariaModel barbearia);
        Task AtualizarBarbearia(Guid idBarbearia, BarbeariaModel barbearia);
        Task AtualizarDonoBarbearia(Guid idBarbearia, string dono);
        Task Apagar(Guid idBarbearia);

    }
}
