using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAplicacaoEstaciona.Util;

namespace WebAplicacaoEstaciona.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
               
        public string Placa { get; set; }

        /// <summary>
        /// Lista os veiculos
        /// </summary>
        /// <returns></returns>
        public List<Veiculo> Listar()
        {
            string json = WebApi.RequestGET("veiculo/veiculos", string.Empty);
            List<Veiculo> listaVeiculo = JsonConvert.DeserializeObject<List<Veiculo>>(json);
            return listaVeiculo;
        }

        public void Cadastrar()
        {
            string jsonData = JsonConvert.SerializeObject(this);     
            string json = WebApi.RequestPOST("veiculo/novo", jsonData);
        }
    }
}
