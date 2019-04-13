using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiEstaciona.DAL
{
    public static class ConexaoBD
    {
        //Dados da conexão MySQL
        private static string Server = "104.41.11.5";
        private static string DtBase = "db_estacionamento";
        private static string User = "rootazure@dbestacinamento";
        private static string Password = "R00tazure";
        private static MySqlConnection Connection;

        //Dados da conexão MySQL
        //private static string Server = "localhost";
        //private static string DtBase = "db_estacionamento";
        //private static string User = "root";
        //private static string Password = "root";
        //private static MySqlConnection Connection;

        //Montando a string de conexão
        static string ConectionString = $"Server={Server};Database={DtBase};Uid={User};Pwd={Password};CharSet=utf8;SslMode=none;";

        public static MySqlConnection GetConection()
        {
            try
            {
                Connection = new MySqlConnection(ConectionString);
                Connection.Open();
                return Connection;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
