using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using WebApiEstaciona.Models;

namespace WebApiEstaciona
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();


            var host = new WebHostBuilder()
          .UseKestrel()
          .UseContentRoot(Directory.GetCurrentDirectory())
          .UseIISIntegration()
          .UseStartup<Startup>()
          .Build();

            //Inicializa com 15 vagas
            new EstacionamentoModel().InicializarEstacionamento(15);

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(true)
                .UseSetting("detailedErrors", "true")
                .UseStartup<Startup>()
                .Build();
    }
}
