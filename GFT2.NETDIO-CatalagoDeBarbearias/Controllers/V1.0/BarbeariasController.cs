using GFT2.NETDIO_CatalagoDeBarbearias.Exceptions;
using GFT2.NETDIO_CatalagoDeBarbearias.Model;
using GFT2.NETDIO_CatalagoDeBarbearias.Service;
using GFT2.NETDIO_CatalagoDeBarbearias.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GFT2.NETDIO_CatalagoDeBarbearias.Controllers.V1._0
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BarbeariasController : ControllerBase
    {
        private readonly IBarbeariaService _barbeariaService; 

        public BarbeariasController(IBarbeariaService barbeariaService)
        {
            _barbeariaService = barbeariaService;
        }


        //Task é um padrão dos metodos nas controller


        /// <summary>
        /// Buscar todas as barbearias de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possivel retornar barbearias sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual pagina esta sendo consultada. Minimo 1</param>
        /// <param name="quantidade">Indica a quantidade de registrtos por pagina. Minimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de barbearias</response>
        /// <response code="204">Caso não haja barbearias</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BarbeariaViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade= 5)
        {
            var barbearias = await _barbeariaService.Obter(pagina, quantidade);
            if(barbearias.Count() == 0)
                return NoContent();
            return Ok(barbearias);
            
        }



        /// <summary>
        /// Busca uma barbearia pelo Id
        /// </summary>
        /// <param name="idBarbearia">Id da barbearia a ser buscada</param>
        /// <response code="200">Retorna a barbearia filtrada</response>
        /// <response code="204">Caso não haja barbearia com este id</response>
        [HttpGet("{idBarbearia:guid}")]
        public async Task<ActionResult<BarbeariaViewModel>> Obter([FromRoute] Guid idBarbearia)
        {
            var barbearia = await _barbeariaService.Obter(idBarbearia);
            if (barbearia == null)
                return NoContent();

            return Ok(barbearia);
        }


        /// <summary>
        /// Inseri uma nova barbearia
        /// </summary>
        /// <param name="barbeariaModel">Objeto barbeariaModel</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BarbeariaViewModel>> InserirBarbearia([FromBody] BarbeariaModel barbeariaModel)
        {
            try
            {
                var barbearia = await _barbeariaService.Inserir(barbeariaModel);
                return Ok(barbeariaModel);
            }
            catch(BarbeariaJaCadastradaException ex)
            {
                return UnprocessableEntity("Já existe uma barbearia com esse nome");
            }
        }


        //put atualiza tudo do objeto
        /// <summary>
        /// Atualiza todos os campos da barbearia
        /// </summary>
        /// <param name="idBarbearia">Id da barbearia a ser atualizada</param>
        /// <param name="barbearia">Objeto barbeariaModel a ser atualizado</param>
        /// <returns></returns>
        [HttpPut("{idBarbearia:guid}")]
        public async Task<ActionResult> AtualizarBarbearia([FromRoute]Guid idBarbearia, [FromBody]BarbeariaModel barbearia)
        {
            try
            {
                await _barbeariaService.AtualizarBarbearia(idBarbearia, barbearia);
                return Ok();
            }
            catch (BarbeariaNaoCadastradaException ex)
            {
                return UnprocessableEntity("Não existe esta barbearia");
            }
        }

        //patch atualiza algo mais especifico do objeto

        /// <summary>
        /// Atualiza apenas o dono da barbearia
        /// </summary>
        /// <param name="idBarbearia">Id da barbearia a ter o dono alterado</param>
        /// <param name="dono">Nome do novo dono</param>
        /// <returns></returns>
        [HttpPatch("{idBarbearia:guid}/dono/{dono}")]
        public async Task<ActionResult> AtualizarDonoBarbearia([FromBody] Guid idBarbearia, [FromRoute] string dono)
        {
            try
            {
                await _barbeariaService.AtualizarDonoBarbearia(idBarbearia, dono);
                return Ok();
            }
            catch(BarbeariaNaoCadastradaException ex)
            {
                return UnprocessableEntity("Não existe esta barbearia");
            }
        }


        /// <summary>
        /// Apaga uma barbearia
        /// </summary>
        /// <param name="idBarbearia">Id da barbearia a ser deletada</param>
        /// <returns></returns>
        [HttpDelete("{idBarbearia:guid}")]
        public async Task<ActionResult> ApagarBarbearia([FromRoute]Guid idBarbearia)
        {
            try
            {
                await _barbeariaService.Apagar(idBarbearia);
                return Ok();
            }
            catch(BarbeariaNaoCadastradaException ex)
            {
                return UnprocessableEntity("Não existe esta barbearia");
            }
        }
    }
}
