using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApiEstaciona.DAL;

namespace WebApiEstaciona.Models
{
    public class VagaModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        
        /// <summary>
        /// Registra Vaga, caso esteja disponível
        /// </summary>
        public List<VagaModel> ListaVagasDisponíveis()
        {
            string sql = "SELECT * FROM dbe_vaga WHERE NOT EXISTS(SELECT 1 FROM dbe_estacionamento WHERE dbs_id_vaga = dbg_id and dbs_estado = 'A')";
            DataTable dados = MetodosAcessoDAL.RetornaDataTable(ConexaoBD.GetConection(), sql);
            List<VagaModel> listadeVagasDisponiveis = new List<VagaModel>();

            foreach (DataRow item in dados.Rows)
            {
                VagaModel vaga = new VagaModel()
                {
                    Id = Convert.ToInt32(item["dbg_id"].ToString()),
                    Descricao = item["dbg_descricao"].ToString(),
                };
                listadeVagasDisponiveis.Add(vaga);
            }
            return listadeVagasDisponiveis;
        }

        /// <summary>
        /// Lista as Vagas Ocupadas
        /// </summary>
        public List<VagaModel> ListaVagasOcupadas()
        {
            string sql = "SELECT * FROM dbe_vaga WHERE EXISTS(SELECT 1 FROM dbe_estacionamento WHERE dbs_id_vaga = dbg_id and dbs_estado = 'A')";
            DataTable dados = MetodosAcessoDAL.RetornaDataTable(ConexaoBD.GetConection(), sql);
            List<VagaModel> listadeVagasDisponiveis = new List<VagaModel>();

            foreach (DataRow item in dados.Rows)
            {
                VagaModel vaga = new VagaModel()
                {
                    Id = Convert.ToInt32(item["dbg_id"].ToString()),
                    Descricao = item["dbg_descricao"].ToString(),
                };
                listadeVagasDisponiveis.Add(vaga);
            }
            return listadeVagasDisponiveis;
        }
    }
}

