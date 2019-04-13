using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiEstaciona.Security
{
    public class Autenticacao
    {
        public static string TOKEN = "12234JHJKHKHDdududjddksksawxcb34568";
        public static string FALHA = "Falha na autenticação, token não informado ou inválido!";
        IHttpContextAccessor _contextHttp;

        public Autenticacao(IHttpContextAccessor context)
        {
            _contextHttp = context;
        }

        public void Autenticar()
        {
            #if (!DEBUG)
                        try
                                {
                                    string tokenRecebido = _contextHttp.HttpContext.Request.Headers["token"].ToString();
                                    if (!string.Equals(TOKEN, tokenRecebido))
                                    {
                                        throw new UnauthorizedAccessException(FALHA);
                                    }
                                }
                                catch (Exception)
                                {
                                    throw new UnauthorizedAccessException(FALHA);
                                }
                            
            #endif
        }
    }
}
