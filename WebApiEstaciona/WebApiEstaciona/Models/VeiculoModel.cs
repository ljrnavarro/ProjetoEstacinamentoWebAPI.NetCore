using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApiEstaciona.DAL;

namespace WebApiEstaciona.Models
{
    public class VeiculoModel
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }

        /// <summary>
        /// Registra o veículo
        /// </summary>
        public void RegistrarVeiculo()
        {
            if (string.IsNullOrEmpty(Modelo) || string.IsNullOrEmpty(Placa))
            {
                throw new Exception("Modelo e Placa do veículo são obrigtórios");
            }

            string sql = $"INSERT INTO dbe_veiculo(dbv_modelo, dbv_placa) values('{Modelo}', '{Placa}')";
            MetodosAcessoDAL.ExecutarComandoSQL(ConexaoBD.GetConection(), sql);
        }

        /// <summary>
        /// Lista os veículos cadastrados
        /// </summary>
        /// <returns></returns>
        public List<VeiculoModel> ListaVeiculos()
        {
            string sql = "select * from dbe_veiculo";
            DataTable dados = MetodosAcessoDAL.RetornaDataTable(ConexaoBD.GetConection(), sql);
            List<VeiculoModel> listadeveiculos = new List<VeiculoModel>();

            foreach (DataRow item in dados.Rows)
            {
                VeiculoModel veiculo = new VeiculoModel()
                {
                    Id = Convert.ToInt32(item["dbv_id"].ToString()),
                    Modelo = item["dbv_modelo"].ToString(),
                    Placa = item["dbv_placa"].ToString(),
                };
                listadeveiculos.Add(veiculo);
            }
            return listadeveiculos;
        }

    }
}
