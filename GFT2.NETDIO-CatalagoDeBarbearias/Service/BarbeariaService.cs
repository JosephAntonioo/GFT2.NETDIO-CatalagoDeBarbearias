using GFT2.NETDIO_CatalagoDeBarbearias.Entity;
using GFT2.NETDIO_CatalagoDeBarbearias.Exceptions;
using GFT2.NETDIO_CatalagoDeBarbearias.Model;
using GFT2.NETDIO_CatalagoDeBarbearias.Repositories;
using GFT2.NETDIO_CatalagoDeBarbearias.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT2.NETDIO_CatalagoDeBarbearias.Service
{
    public class BarbeariaService : IBarbeariaService
    {
        private readonly IBarbeariaRepository _barbeariaRepository;

        public BarbeariaService(IBarbeariaRepository barbeariaRepository)
        {
            _barbeariaRepository = barbeariaRepository;
        }

        public async Task<List<BarbeariaViewModel>> Obter(int pagina, int quantidade)
        {
            var barbearias = await _barbeariaRepository.Obter(pagina, quantidade);
            return barbearias.Select(barbearia => new BarbeariaViewModel
            {
                Id = barbearia.Id,
                Nome = barbearia.Nome,
                Dono = barbearia.Dono,
                ValorMensalidade = barbearia.ValorMensalidade,
                MesesContrato = barbearia.MesesContrato
            }).ToList();
        }

        public async Task<BarbeariaViewModel> Obter(Guid id)
        {
            var barbearia = await _barbeariaRepository.Obter(id);
            if (barbearia == null)
                return null;
            return new BarbeariaViewModel
            {
                Id = barbearia.Id,
                Nome = barbearia.Nome,
                Dono = barbearia.Dono,
                ValorMensalidade = barbearia.ValorMensalidade,
                MesesContrato = barbearia.MesesContrato
            };
        }

        public async Task<BarbeariaViewModel> Inserir(BarbeariaModel barbearia)
        {
            var entidade = await _barbeariaRepository.Obter(barbearia.Nome, barbearia.Dono);
            if (entidade.Count > 0)
                throw new BarbeariaJaCadastradaException();

            var entidadeInsert = new Barbearia
            {
                Id = Guid.NewGuid(),
                Nome = barbearia.Nome,
                Dono = barbearia.Dono,
                ValorMensalidade = barbearia.ValorMensalidade,
                MesesContrato = barbearia.MesesContrato
            };

            await _barbeariaRepository.Inserir(entidadeInsert);

            return new BarbeariaViewModel
            {
                Id = entidadeInsert.Id,
                Nome = entidadeInsert.Nome,
                Dono = entidadeInsert.Dono,
                ValorMensalidade = entidadeInsert.ValorMensalidade,
                MesesContrato = entidadeInsert.MesesContrato
            };
        }

        public async Task AtualizarBarbearia(Guid id, BarbeariaModel barbearia)
        {
            var entidade = await _barbeariaRepository.Obter(id);
            if (entidade == null)
                throw new BarbeariaNaoCadastradaException();
            entidade.Nome = barbearia.Nome;
            entidade.Dono = barbearia.Dono;
            entidade.ValorMensalidade = barbearia.ValorMensalidade;
            entidade.MesesContrato = barbearia.MesesContrato;

            await _barbeariaRepository.Atualizar(entidade);
        }

        
        public async Task AtualizarDonoBarbearia(Guid id, string dono)
        {
            var entidade = await _barbeariaRepository.Obter(id);
            if (entidade == null)
                throw new BarbeariaNaoCadastradaException();
            entidade.Dono = dono;

            await _barbeariaRepository.Atualizar(entidade);
        }
        public async Task Apagar(Guid id)
        {
            var barbearia = _barbeariaRepository.Obter(id);

            if (barbearia == null)
                throw new BarbeariaNaoCadastradaException();

            await _barbeariaRepository.Remover(id);
        }

        public void Dispose()
        {
            _barbeariaRepository?.Dispose();
        }


    }
}
