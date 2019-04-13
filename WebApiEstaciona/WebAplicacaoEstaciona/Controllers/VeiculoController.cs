using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAplicacaoEstaciona.Models;

namespace WebAplicacaoEstaciona.Controllers
{
    public class VeiculoController : Controller
    {
        // GET: Veiculo
        public ActionResult Index()
        {
            Veiculo veiculo = new Veiculo();
            return View(veiculo.Listar());
        }

        // GET: Veiculo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Veiculo/Novo
        public ActionResult Novo()
        {
            return View();
        }

        // POST: Veiculo/Novo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Novo(Veiculo veiculo)
        {
            try
            {
                veiculo.Placa = veiculo.Placa.Replace("-", "").ToUpper();
                veiculo.Cadastrar();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Veiculo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Veiculo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Veiculo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Veiculo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}