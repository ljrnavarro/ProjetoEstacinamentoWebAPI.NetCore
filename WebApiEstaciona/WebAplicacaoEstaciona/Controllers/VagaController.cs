using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAplicacaoEstaciona.Models;

namespace WebAplicacaoEstaciona.Controllers
{
    public class VagaController : Controller
    {
        // GET: Vaga
        public ActionResult Disponiveis()
        {
            Vaga vagas = new Vaga();
            return View(vagas.Listar());
        }

        public ActionResult Ocupadas()
        {
            VagasCompleto vagas = new VagasCompleto();
            return View(vagas.Listar());
        }

        public ActionResult Impressao()
        {
            VagasCompleto vagas = new VagasCompleto();
            var pdf = new Rotativa.AspNetCore.ViewAsPdf(vagas.Listar());
            return pdf;
        }
    }
}