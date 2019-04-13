using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAplicacaoEstaciona.Util;

namespace WebAplicacaoEstaciona.Models
{
    public class VagasCompleto
    {
        public string idEstacionamento;
        public string modelo;
        public string placa;
        public string vaga;
        public string entrada;


        public List<VagasCompleto> Listar()
        {
            string json = WebApi.RequestGET("vaga/ocupadas", string.Empty);
            List<VagasCompleto> listaVeiculo = JsonConvert.DeserializeObject<List<VagasCompleto>>(json);
            return listaVeiculo;
        }
    }
}
