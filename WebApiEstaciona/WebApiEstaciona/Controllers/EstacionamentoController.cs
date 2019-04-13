using System;
using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApiEstaciona.Models;
using WebApiEstaciona.Security;
using WebApiEstaciona.Util;

namespace WebApiEstaciona.Controllers
{
    /// <summary>
    /// Classe principal da aplicação responsável por manter os veícuos e suas vagas
    /// </summary>
    [Route("webapiestaciona/[Controller]")]
    public class EstacionamentoController : Controller
    {
        Autenticacao AutenticacaoAPI;

        /// <summary>
        /// Ininicializador
        /// </summary>
        /// <param name="contexto"></param>
        public EstacionamentoController(Microsoft.AspNetCore.Http.IHttpContextAccessor contexto)
        {
            AutenticacaoAPI = new Autenticacao(contexto);
        }

        /// <summary>
        /// Serviço responsável por registrar a Vaga sendo passado no Body o modelo Estacionamento
        /// </summary>
        /// <param name="dados"></param>
        /// <returns></returns>
        // POST webapiestaciona/novo
        [HttpPost]
        [Route("novo")]
        public IActionResult Novo([FromBody]EstacionamentoModel dados)
        {
            ResultMessageServices retorno = new ResultMessageServices();
            try
            {
                AutenticacaoAPI.Autenticar();
                dados.RegistrarVagaEstacionamento();
                retorno.Result = true;
                retorno.ErroMessage = string.Empty;
            }
            catch (UnauthorizedAccessException erroautorizacao)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, erroautorizacao.Message);
            }
            catch (Exception erro)
            {
                retorno.Result = false;
                retorno.ErroMessage = "Erro ao tentar registrar vaga. " + erro.Message.ToString();
            }
            return new OkObjectResult(retorno);
        }

        /// <summary>
        /// Serviço responsável por efetuar o pagamento, após isso a vaga no estacionamento é Finalizada e é gerado um Tiket de Pagamento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST webapiestaciona/pagar/
        [HttpPost]
        [Route("pagar")]
        public IActionResult Pagar([FromBody]int id)
        {
            ResultMessageServices retorno = new ResultMessageServices();
            try
            {
                AutenticacaoAPI.Autenticar();
                new EstacionamentoModel().RegistrarPagamento(id);
                retorno.Result = true;
                retorno.ErroMessage = string.Empty;
            }
            catch (UnauthorizedAccessException erroautorizacao)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, erroautorizacao.Message);
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErroMessage = "Erro ao tentar realizar pagamento. " + ex.Message.ToString();
            }
            return new OkObjectResult(retorno);
        }

        /// <summary>
        /// Serviço responsável por Emitir uum relatório completo + financeiro do Estacionamento
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //POST webapiestaciona/estacionamentos/gerencial
        [HttpPost]
        [Route("estacionamentos/gerencial")]
        public IActionResult Gerencial([FromBody]JObject data)
        {
            ResultMessageServices retorno = new ResultMessageServices();
            DateTime dtInicio = DateTime.Parse((string)data.SelectToken("DtInicio"));
            DateTime dtFim = DateTime.Parse((string)data.SelectToken("DtFim"));
            try
            {
                AutenticacaoAPI.Autenticar();
                retorno.Result = true;
                retorno.ErroMessage = string.Empty;
                return new OkObjectResult(new EstacionamentoModel().ObtemRelatorioCompletoLista(dtInicio,dtFim));
            }
            catch (UnauthorizedAccessException erroautorizacao)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, erroautorizacao.Message);
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErroMessage = "Erro ao buscar dados. " + ex.Message.ToString();
            }
            return new OkObjectResult(retorno);
        }

        /// <summary>
        /// Obtem uma lista completa do meu estacionamento
        /// </summary>
        /// <returns></returns>
        //GET webapiestaciona/estacionamentos}
        [HttpGet]
        [Route("estacionamentos")]
        public IActionResult Estacionamentos()
        {
            ResultMessageServices retorno = new ResultMessageServices();
           
            try
            {
                AutenticacaoAPI.Autenticar();
                retorno.Result = true;
                retorno.ErroMessage = string.Empty;
                return new OkObjectResult(new EstacionamentoModel().ObtemEstacionamentos());
            }
            catch (UnauthorizedAccessException erroautorizacao)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, erroautorizacao.Message);
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErroMessage = "Erro ao buscar dados. " + ex.Message.ToString();
            }
            return new OkObjectResult(retorno);
        }
    }
}
