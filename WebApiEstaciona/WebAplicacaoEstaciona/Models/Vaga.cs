using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAplicacaoEstaciona.Util;

namespace WebAplicacaoEstaciona.Models
{
    public class Vaga
    {
        public int Id { get; set; }
        public string Descricao { get; set; }


        public List<Vaga> Listar()
        {
            string json = WebApi.RequestGET("vaga/disponiveis", string.Empty);
            List<Vaga> listaVaga = JsonConvert.DeserializeObject<List<Vaga>>(json);
            return listaVaga;
        }
    }
}
