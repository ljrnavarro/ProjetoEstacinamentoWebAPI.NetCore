using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiEstaciona.DAL;
using WebApiEstaciona.Models;
using WebApiEstaciona.Security;
using WebApiEstaciona.Util;

namespace WebApiEstaciona.Controllers
{
    [Route("webapiestaciona/[Controller]")]
    public class VagaController : Controller
    {
        Autenticacao AutenticacaoAPI;

        public VagaController(Microsoft.AspNetCore.Http.IHttpContextAccessor contexto)
        {
            AutenticacaoAPI = new Autenticacao(contexto);
        }

        /// <summary>
        /// Obtem lista de vagas que estão usadas no estacionamento (posição das vagas)
        /// </summary>
        /// <returns></returns>
        // GET webapiestaciona/ocupadas
        [HttpGet]
        [Route("ocupadas")]
        public IActionResult Ocupadas()
        {
            try
            {
                AutenticacaoAPI.Autenticar();
                return new OkObjectResult(new EstacionamentoModel().ListaVagasOcupadas());
            }catch(UnauthorizedAccessException erro)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, erro.Message);
            }
        }

        /// <summary>
        /// Obtem lista de vagas que estão disponíveis
        /// </summary>
        /// <returns></returns>
        // GET webapiestaciona/disponiveis
        [HttpGet]
        [Route("disponiveis")]
        public IActionResult Disponiveis()
        {
            try
            {
                AutenticacaoAPI.Autenticar();
                return new OkObjectResult(new VagaModel().ListaVagasDisponíveis());
            }
            catch (UnauthorizedAccessException erro)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, erro.Message);
            }
        }

        /// <summary>
        /// Obtem a quantidade de vagas disponíveis
        /// </summary>
        /// <returns></returns>
        // GET webapiestaciona/disponiveis/quantidade
        [HttpGet]
        [Route("disponiveis/quantidade")]
        public IActionResult QuantVagasDisponiveis()
        {
            try
            {
                AutenticacaoAPI.Autenticar();
                int QuantVagasDisp = new VagaModel().ListaVagasDisponíveis().Count;
                return new OkObjectResult(QuantVagasDisp);
            }
            catch (UnauthorizedAccessException erro)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, erro.Message);
            }
        }
    }
}
