using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiEstaciona.DAL
{
    public static class MetodosAcessoDAL
    {
        /// <summary>
        /// Comandos DELETE, INSERT e UPDATE
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="SQL"></param>
        public static void ExecutarComandoSQL(MySqlConnection connection, string SQL)
        {
            using (MySqlCommand comando = new MySqlCommand(SQL, connection))
            {
                comando.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Retorna datatable
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public static DataTable RetornaDataTable(MySqlConnection connection,string SQL)
        {
            DataTable dados = new DataTable();
            using (MySqlCommand comando = new MySqlCommand(SQL, connection))
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(comando);
                dataAdapter.Fill(dados);
                return dados;
            }
        }

        /// <summary>
        /// Execute escalar
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public static string ReturnValue(MySqlConnection connection, string SQL)
        {
            using (MySqlCommand comando = new MySqlCommand(SQL, connection))
            {
               object result =  comando.ExecuteScalar();

                if (result != null)
                {
                    int r = Convert.ToInt32(result);
                    return r.ToString();
                }
                else
                    return null;
            }
        }
    }
}
