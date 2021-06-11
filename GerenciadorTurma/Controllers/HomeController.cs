using GerenciadorTurma.Data;
using GerenciadorTurma.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GerenciadorTurma.Controllers
{
    public class HomeController : Controller
    {
        public BancoDeDados BancoDeDados { get; }

        public HomeController(BancoDeDados bancoDeDados)
        {
            BancoDeDados = bancoDeDados;
        }

        public IActionResult Index()
        {
            var viewModel = new Models.Home.IndexModel();

            viewModel.QuantidadeDeAlunos = BancoDeDados.Alunos.Count();

            return View(viewModel);
        }

        public class Modelo
        {
            public List<Aluno> Alunos { get; set; }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
