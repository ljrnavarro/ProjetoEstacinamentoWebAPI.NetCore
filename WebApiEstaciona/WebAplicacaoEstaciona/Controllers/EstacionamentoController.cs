using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using WebAplicacaoEstaciona.Models;

namespace WebAplicacaoEstaciona.Controllers
{
    public class EstacionamentoController : Controller
    {
        // GET: Estacionamento
        public ActionResult Index()
        {
            return View();
        }

        // GET: Estacionamento/Estacionar
        public ActionResult Estacionar(int id)
        {
            Estacionamento estacionamento = new Estacionamento();
            var vagasDisponiveis = new Vaga().Listar(); //.Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString()});
            ViewBag.ListaVagas = vagasDisponiveis;
            estacionamento.IdVeiculo = ViewBag.IdVeiculo = id;
            estacionamento.DataHora_Entrada = DateTime.Now;
            estacionamento.Estado = "A";
            return View(estacionamento);
        }

        // POST: Estacionamento/Estacionar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Estacionar(Estacionamento estacionamento)
        {
            try
            {
                if (estacionamento.IdVaga == 0)
                {
                    TempData["Message"] = "ERRO! Necessário escolher uma Vaga";
                    return RedirectToAction("Index", "veiculo");
                }

                estacionamento.Cadastrar();
                TempData["Message"] = "Veículo estacionado";
                return RedirectToAction("Index", "veiculo");
            }
            catch
            {
                return View();
            }
        }

        // GET: Estacionamento/Pagar
        public ActionResult Pagar(int id)
        {
            Estacionamento estacionamento = new Estacionamento().Listar().Where(d => d.Id == id).FirstOrDefault();
            #region calculando valor a pagar
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
            ViewBag.ValoraPagar = valorCobrado;
            estacionamento.IdVeiculo = ViewBag.IdVeiculo = id;
            estacionamento.DataHora_Saida = DateTime.Now;
            estacionamento.Estado = "P";
            return View(estacionamento);
        }

        // POST: Estacionamento/Pagar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pagar(Estacionamento estacionamento)
        {
            try
            {
                estacionamento.PagarEstacionamento();
                return RedirectToAction("Ocupadas", "vaga");
            }
            catch (Exception erro)
            {
                TempData["Message"] = $"{erro.Message.ToString()}";
                return View("Ocupadas", "vaga");
            }
        }

        public ActionResult FormularioPesquisa()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FormularioPesquisa(IFormCollection collection)
        {
            try
            {
                DateTime dtInicial = DateTime.Parse(collection["DataInicial"]);
                DateTime dtFinal = DateTime.Parse(collection["DataFinal"]);
                return RedirectToAction("RelatorioCompleto", new { dtInicio = dtInicial, dtFim = dtFinal });
            }
            catch (Exception erro)
            {
                TempData["Message"] = $"{"Erro:"+ erro.Message.ToString()}";
                return View();
            }
        }


        [HttpGet]
        public ActionResult RelatorioCompleto(DateTime dtInicio, DateTime dtFim)
        {
            List<ListaEstacionamentoCompletoRelatorio> listaCompleta = 
                new ListaEstacionamentoCompletoRelatorio().RelatorioCompletoLista(dtInicio,dtFim);
            Decimal valortotal = (Decimal)listaCompleta.Select(l => Convert.ToDecimal(l.valorPago)).Sum();
            ViewBag.ValorTotal = valortotal;
            ViewBag.DataInicial = dtInicio;
            ViewBag.DataFinal = dtFim;
            return View(listaCompleta);
        }

        public IActionResult ImprimirPDF(DateTime dtInicio, DateTime dtFim)
        {
            List<ListaEstacionamentoCompletoRelatorio> listaCompleta =
                new ListaEstacionamentoCompletoRelatorio().RelatorioCompletoLista(dtInicio, dtFim);
            Decimal valortotal = (Decimal)listaCompleta.Select(l => Convert.ToDecimal(l.valorPago)).Sum();
            var pdf = new ViewAsPdf(listaCompleta);
            return pdf;
        }
    }
}