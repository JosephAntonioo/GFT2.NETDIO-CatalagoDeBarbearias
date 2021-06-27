using GFT2.NETDIO_CatalagoDeBarbearias.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT2.NETDIO_CatalagoDeBarbearias.Repositories
{
    public class BarbeariaRepository : IBarbeariaRepository
    {
        private static Dictionary<Guid, Barbearia> barbearias = new Dictionary<Guid, Barbearia>()
        {
            {Guid.Parse("4a95e623-3b0c-47c2-a6c6-4c27f8631c33"), new Barbearia{ Id = Guid.Parse("4a95e623-3b0c-47c2-a6c6-4c27f8631c33"), Nome = "teste1", Dono = "teste1", ValorMensalidade = 60.0, MesesContrato = 6} },
            {Guid.Parse("c70fd2f8-adec-496a-a3f3-f1ebb4a9add3"), new Barbearia{ Id = Guid.Parse("c70fd2f8-adec-496a-a3f3-f1ebb4a9add3"), Nome = "teste2", Dono = "teste2", ValorMensalidade = 70.0, MesesContrato = 7} },
            {Guid.Parse("c30b6add-a7f1-4b47-9072-1ddf519d63bc"), new Barbearia{ Id = Guid.Parse("c30b6add-a7f1-4b47-9072-1ddf519d63bc"), Nome = "teste3", Dono = "teste3", ValorMensalidade = 80.0, MesesContrato = 8} },
            {Guid.Parse("ec64c867-d3fe-458f-846e-c76cf9b3bbf5"), new Barbearia{ Id = Guid.Parse("ec64c867-d3fe-458f-846e-c76cf9b3bbf5"), Nome = "teste4", Dono = "teste4", ValorMensalidade = 90.0, MesesContrato = 9} },
        };


        public Task Atualizar(Barbearia barbearia)
        {
            barbearias[barbearia.Id] = barbearia;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //fechar conexao com o banco
        }

        public Task Inserir(Barbearia barbearia)
        {
            barbearias.Add(barbearia.Id, barbearia);
            return Task.CompletedTask;
        }

        public Task<List<Barbearia>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(barbearias.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Barbearia> Obter(Guid id)
        {
            if (!barbearias.ContainsKey(id))
                return null;
            return Task.FromResult(barbearias[id]);
        }

        public Task<List<Barbearia>> Obter(string nome, string dono)
        {
            return Task.FromResult(barbearias.Values.Where(barbearia => barbearia.Nome.Equals(nome) && barbearia.Dono.Equals(dono)).ToList());
        }

        public Task Remover(Guid id)
        {
            barbearias.Remove(id);
            return Task.CompletedTask;
        }
    }
}
