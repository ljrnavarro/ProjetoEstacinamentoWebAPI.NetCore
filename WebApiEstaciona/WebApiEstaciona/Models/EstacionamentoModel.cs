using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApiEstaciona.DAL;

namespace WebApiEstaciona.Models
{
    public class EstacionamentoModel
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public int IdVaga { get; set; }
        public int IdVeiculo { get; set; }
        public DateTime DataHora_Entrada { get; set; }
        public DateTime? DataHora_Saida { get; set; }

        /// <summary>
        /// Inicializa um novo estacinamento
        /// </summary>
        /// <param name="qntVagas"></param>
        public void InicializarEstacionamento(int qntVagas)
        {
            #region Verificando se a vaga solicitada está disponível
            string sqlconsulta = $"SELECT COUNT(*) FROM dbe_vaga";

            Int32 retornoQnt = Convert.ToInt32(MetodosAcessoDAL.ReturnValue(ConexaoBD.GetConection(), sqlconsulta));
            if (retornoQnt == 0)
            {
                MetodosAcessoDAL.ExecutarComandoSQL(ConexaoBD.GetConection(), sqlconsulta);

                for (int i = 1; i <= qntVagas; i++)
                {
                    string descricaovaga = "A" + i.ToString();
                    string sql = $"INSERT INTO dbe_vaga(dbg_descricao) values('{descricaovaga}')";
                    MetodosAcessoDAL.ExecutarComandoSQL(ConexaoBD.GetConection(), sql);
                }
            }
            #endregion
        }

        /// <summary>
        /// Obtem a lista de todos os estacionamentos
        /// </summary>
        /// <returns></returns>
        public List<EstacionamentoModel> ObtemEstacionamentos()
        {
            string sql = "SELECT * FROM dbe_estacionamento";

            DataTable dados = MetodosAcessoDAL.RetornaDataTable(ConexaoBD.GetConection(), sql);

            if (dados.Rows.Count == 0)
            {
                throw new Exception("Consulta não retornou dados");
            }

            List<EstacionamentoModel> listaEstacionamento = new List<EstacionamentoModel>();
            foreach (DataRow item in dados.Rows)
            {
                EstacionamentoModel estaciona = new EstacionamentoModel();
                estaciona.Id = Convert.ToInt32(item["dbs_id"].ToString());
                estaciona.IdVaga = Convert.ToInt32(item["dbs_id_vaga"].ToString());
                estaciona.IdVeiculo = Convert.ToInt32(item["dbs_id_veiculo"].ToString());
                estaciona.DataHora_Entrada = DateTime.Parse(item["dbs_datahora_entrada"].ToString());
                if (!string.IsNullOrEmpty(item["dbs_datahora_saida"].ToString()))
                    estaciona.DataHora_Saida = DateTime.Parse(item["dbs_datahora_saida"].ToString());
                estaciona.Estado = Convert.ToString(item["dbs_estado"].ToString());
                listaEstacionamento.Add(estaciona);
            }
            return listaEstacionamento;
        }

        /// <summary>
        /// Registra Vaga, caso esteja disponível
        /// </summary>
        public void RegistrarVagaEstacionamento()
        {
            if (IdVaga == 0 || IdVeiculo == 0)
            {
                throw new Exception("Veículo e Vaga são obrigatórios");
            }

            #region Verificando se a vaga solicitada está disponível
            string sqlconsulta = $"SELECT COUNT(*) FROM dbe_vaga WHERE dbg_id = '{IdVaga}' AND " +
                                "NOT EXISTS(SELECT 1 FROM dbe_estacionamento WHERE dbs_id_vaga = dbg_id and dbs_estado = 'A')";

            Int32 retornoQntDisponivel = Convert.ToInt32(MetodosAcessoDAL.ReturnValue(ConexaoBD.GetConection(), sqlconsulta));
            if (retornoQntDisponivel == 0)
            {
                throw new Exception("Vaga não disponível");
            }
            #endregion

            #region Verificando se o veiculo já está em uma vaga
            string sqlconsultaVeiculo = $"SELECT COUNT(*) FROM dbe_estacionamento e WHERE e.dbs_id_veiculo = {IdVeiculo} and dbs_estado = 'A'";
            Int32 veiculoJaEstaNoEstacionamento = Convert.ToInt32(MetodosAcessoDAL.ReturnValue(ConexaoBD.GetConection(), sqlconsultaVeiculo));
            if (veiculoJaEstaNoEstacionamento > 0)
            {
                throw new Exception("Veiculo já se encontra no estacionamento");
            }
            #endregion

            #region Exexutado a inserção
            string sql = $"INSERT INTO dbe_estacionamento(dbs_estado, dbs_id_vaga, dbs_id_veiculo, dbs_datahora_entrada) values('A', {IdVaga}, {IdVeiculo}, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";
            MetodosAcessoDAL.ExecutarComandoSQL(ConexaoBD.GetConection(), sql);
            #endregion
        }

        /// <summary>
        /// Lista as vagas ocupadas com as informações do veículo
        /// </summary>
        /// <returns></returns>
        public List<ListaEstacionamentoCompleto> ListaVagasOcupadas()
        {
            string sql = "SELECT e.dbs_id as idestacionamento, ve.dbv_modelo as modelo,ve.dbv_placa placa, va.dbg_descricao vaga, e.dbs_datahora_entrada entrada FROM " +
                         "dbe_estacionamento e, dbe_veiculo ve, dbe_vaga va WHERE " +
                         "e.dbs_estado = 'A' and e.dbs_id_veiculo = ve.dbv_id and e.dbs_id_vaga = va.dbg_id and " +
                         "EXISTS(SELECT 1 FROM dbe_estacionamento WHERE dbs_id_vaga = dbg_id and dbs_estado = 'A')";

            DataTable dados = MetodosAcessoDAL.RetornaDataTable(ConexaoBD.GetConection(), sql);
            List<ListaEstacionamentoCompleto> listadeVagasOcupadas = new List<ListaEstacionamentoCompleto>();

            foreach (DataRow item in dados.Rows)
            {
                ListaEstacionamentoCompleto vaga = new ListaEstacionamentoCompleto()
                {
                    idEstacionamento = item["idestacionamento"].ToString(),
                    modelo = item["modelo"].ToString(),
                    placa = item["placa"].ToString(),
                    vaga = item["vaga"].ToString(),
                    entrada = item["entrada"].ToString(),
                };
                listadeVagasOcupadas.Add(vaga);
            }
            return listadeVagasOcupadas;
        }

        /// <summary>
        /// Registra o pagament baseado no id do Estacionamento
        /// </summary>
        /// <param name="id"></param>
        public void RegistrarPagamento(int id)
        {
            #region Captura o valor devido
            string sql = $"SELECT * FROM dbe_estacionamento e WHERE e.dbs_id = {id}";
            DataTable dados = MetodosAcessoDAL.RetornaDataTable(ConexaoBD.GetConection(), sql);
            EstacionamentoModel estacionamento = new EstacionamentoModel()
            {
                Id = Convert.ToInt32(dados.Rows[0]["dbs_id"].ToString()),
                DataHora_Entrada = DateTime.Parse(dados.Rows[0]["dbs_datahora_entrada"].ToString())
            };


            TimeSpan span = DateTime.Now.Subtract(estacionamento.DataHora_Entrada);
            double qntHorasAlocadas = Math.Truncate(span.TotalHours);
            decimal valorCobrado;

            if (qntHorasAlocadas <= 3)
                valorCobrado = 7;
            else
            {
                valorCobrado = Math.Round(Convert.ToDecimal(7 + ((qntHorasAlocadas - 3) * 3)), 2);
            }
            #endregion

            #region Atualiza Estacionamento
            string sqlUpdateEstac = $"UPDATE dbe_estacionamento e set e.dbs_estado = 'P', e.dbs_datahora_saida = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' where e.dbs_id = {id}";
            MetodosAcessoDAL.ExecutarComandoSQL(ConexaoBD.GetConection(), sqlUpdateEstac);
            #endregion

            #region Insere o Tiket de Pagamento
            string sqlTiketPagamento = $"INSERT INTO dbe_tiket_pagamento(dbp_dataPagamento, dbp_pago, dbp_valor, dbp_id_estacionamento) " +
                                       $"values('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"S"}',{valorCobrado.ToString()},{id})";
            MetodosAcessoDAL.ExecutarComandoSQL(ConexaoBD.GetConection(), sqlTiketPagamento);
            #endregion
        }

        /// <summary>
        /// ObtemRelatorioCompleto, com dados do estacionamento, pagamentos, veículos e etc.
        /// baseado em datas de entrada
        /// </summary>
        /// <param name="id"></param>
        public List<ListaEstacionamentoCompletoRelatorio> ObtemRelatorioCompletoLista(DateTime datainicial, DateTime datafinal)
        {
            #region Captura os dados do relatório
            string sql = "SELECT * " +
                        "FROM dbe_tiket_pagamento tp," +
                             "dbe_veiculo ve," +
                            "dbe_vaga va," +
                             "dbe_estacionamento e " +
                        "WHERE " +
                             "e.dbs_id = tp.dbp_id_estacionamento AND " +
                             "e.dbs_id_veiculo = ve.dbv_id AND " +
                             "e.dbs_id_vaga = va.dbg_id AND " +
                             $"DATE(tp.dbp_dataPagamento) between '{datainicial.ToString("yyyy-MM-dd")}' and '{datafinal.ToString("yyyy-MM-dd")}'";

            DataTable dados = MetodosAcessoDAL.RetornaDataTable(ConexaoBD.GetConection(), sql);
            #endregion

            if (dados.Rows.Count == 0)
            {
                throw new Exception("Consulta não retornou dados");
            }

            List<ListaEstacionamentoCompletoRelatorio> listaRelatorio =
             (from DataRow dr in dados.Rows
              select new ListaEstacionamentoCompletoRelatorio()
              {
                  Estacionamento = Convert.ToString(dr["dbp_id_estacionamento"].ToString()),
                  modelo = Convert.ToString(dr["dbv_modelo"].ToString()),
                  placa = Convert.ToString(dr["dbv_placa"].ToString()),
                  vaga = Convert.ToString(dr["dbg_descricao"].ToString()),
                  entrada = Convert.ToString(dr["dbs_datahora_entrada"].ToString()),
                  saida = Convert.ToString(dr["dbs_datahora_saida"].ToString()),
                  valorPago = Convert.ToString(dr["dbp_valor"].ToString()),
              }).ToList();

            return listaRelatorio;
        }
    }
}

public class ListaEstacionamentoCompleto
{
    public string idEstacionamento;
    public string modelo;
    public string placa;
    public string vaga;
    public string entrada;
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
}
