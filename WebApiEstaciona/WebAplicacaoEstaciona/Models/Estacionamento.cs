using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAplicacaoEstaciona.Util;

namespace WebAplicacaoEstaciona.Models
{
    public class Estacionamento
    {
        public int Id { get; set; }
        public string Estado { get; set; }

        [Required(ErrorMessage = "Vaga é obrigatória")]
        public int IdVaga { get; set; }
        public int IdVeiculo { get; set; }
        public DateTime DataHora_Entrada { get; set; }
        public DateTime? DataHora_Saida { get; set; }

        /// <summary>
        /// Cadastra o veículo na Vaga
        /// </summary>
        public void Cadastrar()
        {
            string jsonData = JsonConvert.SerializeObject(this);
            string json = WebApi.RequestPOST("estacionamento/novo", jsonData);
        }

        /// <summary>
        /// Efetua o pagamento do estacionamento
        /// </summary>
        public void PagarEstacionamento()
        {
            string jsonData = JsonConvert.SerializeObject(this.Id);
            string json = WebApi.RequestPOST("estacionamento/pagar", jsonData);
        }

        /// <summary>
        /// Obtem lista de stacionamento
        /// </summary>
        /// <returns></returns>
        public List<Estacionamento> Listar()
        {
            string json = WebApi.RequestGET("estacionamento/estacionamentos", string.Empty);
            List<Estacionamento> listaestacionamento = JsonConvert.DeserializeObject<List<Estacionamento>>(json);
            return listaestacionamento;
        }
    }

    public class ListaEstacionamentoCompletoRelatorio
    {
        public string Estacionamento;
        public string modelo;
        public string placa;
        public string vaga;
        public string entrada;
        public string saida;
        public string valorPago;

        /// <summary>
        /// Obtem o relatório completo do estacionamento
        /// </summary>
        /// <returns></returns>
        public List<ListaEstacionamentoCompletoRelatorio> RelatorioCompletoLista(DateTime datainicial, DateTime datafinal)
        {
            DadosFormularioPesquisa dados = new DadosFormularioPesquisa()
            {
                DtInicio = Convert.ToDateTime(datainicial.ToString("yyyy-MM-dd HH:mm:ss")),
                DtFim = Convert.ToDateTime(datafinal.ToString("yyyy-MM-dd HH:mm:ss"))
            };
            string jsonData = JsonConvert.SerializeObject(dados, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" });
            string json = WebApi.RequestPOST("estacionamento/estacionamentos/gerencial", jsonData);
            List<ListaEstacionamentoCompletoRelatorio> lista = JsonConvert.DeserializeObject<List<ListaEstacionamentoCompletoRelatorio>>(json);
            return lista;
        }
    }

    public class DadosFormularioPesquisa
    {
        public DateTime DtInicio;
        public DateTime DtFim;
    }

}
