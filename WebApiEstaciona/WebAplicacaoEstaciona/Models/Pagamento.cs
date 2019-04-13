using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAplicacaoEstaciona.Util;

namespace WebAplicacaoEstaciona.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public DateTime DataPagamento { get; set; }
        public string PagStatus { get; set; }
        public Decimal ValorPago { get; set; }               
        public int IdEstacionamento { get; set; }
    }


}
