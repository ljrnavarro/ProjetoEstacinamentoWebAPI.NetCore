using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebAplicacaoEstaciona.Util
{
    public class WebApi
    {
        public static string URI = "http://localhost:1388/webapiestaciona/";
        public static string TOKEN = "12234JHJKHKHDdududjddksksawxcb34568";
        public static string RequestGET(string metodo, string parametros)
        {
            var request = (HttpWebRequest)WebRequest.Create(URI + metodo + "/" + parametros);
            request.Headers.Add("Token", TOKEN);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }

        public static string RequestPOST(string metodo, string jsonData)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(URI + metodo);
                var data = Encoding.UTF8.GetBytes(jsonData);
                request.Method = "POST";
                request.Headers.Add("Token", TOKEN);
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }catch
            {
                throw;
            }

        }

        public static string RequestGETParamas(string metodo, string jsonData)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(URI + metodo);
                request.Headers.Add("Token", TOKEN);
                request.ContentType = "application/json";
                var data = Encoding.UTF8.GetBytes(jsonData);
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }catch
            {
                throw;
            }
        }
    }
}
