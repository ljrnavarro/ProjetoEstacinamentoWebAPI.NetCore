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
    public class VeiculoController : Controller
    {
        Autenticacao AutenticacaoAPI;

        public VeiculoController(Microsoft.AspNetCore.Http.IHttpContextAccessor contexto)
        {
            AutenticacaoAPI = new Autenticacao(contexto);
        }

        /// <summary>
        /// Lista todos os veículos
        /// </summary>
        /// <returns></returns>
        // GET webapiestaciona/veiculos
        [HttpGet]
        [Route("veiculos")]
        public IActionResult Veiculos()
        {
            try
            {
                AutenticacaoAPI.Autenticar();
                return new OkObjectResult(new VeiculoModel().ListaVeiculos());
            }
            catch (UnauthorizedAccessException erroautentic)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, erroautentic.Message);
            }

        }

        /// <summary>
        /// Captura dados de um veículo específico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET webapiestaciona/veiculo/{id}
        [HttpGet]
        [Route("veiculo/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                AutenticacaoAPI.Autenticar();
                string sql = $"select * from dbe_veiculo where dbv_id = {id}";
                DataTable dados = MetodosAcessoDAL.RetornaDataTable(ConexaoBD.GetConection(), sql);
                return new OkObjectResult(dados.Rows[0]["dbv_modelo"].ToString());
            }
            catch (UnauthorizedAccessException erroAutentic)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, erroAutentic.Message);
            }
        }

        /// <summary>
        /// Cadastra um veículo
        /// </summary>
        /// <param name="dados"></param>
        /// <returns></returns>
        // POST webapiestaciona/novo
        [HttpPost]
        [Route("novo")]
        public IActionResult Novo([FromBody]VeiculoModel dados)
        {
            ResultMessageServices retorno = new ResultMessageServices();
            try
            {
                AutenticacaoAPI.Autenticar();
                dados.RegistrarVeiculo();
                retorno.Result = true;
                retorno.ErroMessage = string.Empty;
            }
            catch (UnauthorizedAccessException erroAutentic)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, erroAutentic.Message);
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErroMessage = "Erro ao tentar registrar veículo. " + ex.Message.ToString();
            }
            return new OkObjectResult(retorno);
        }
    }
}
