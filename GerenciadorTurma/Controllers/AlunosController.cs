using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorTurma.Models;
using GerenciadorTurma.Data;

namespace GerenciadorTurma.Controllers
{
    public class AlunosController : Controller
    {
        public AlunosController(BancoDeDados bancoDeDados)
        {
            BancoDeDados = bancoDeDados;
        }

        private BancoDeDados BancoDeDados { get; set; }

        [HttpGet]
        public ActionResult Index(string nome)
        {
            var model = new AlunosModel();
            
            if (string.IsNullOrWhiteSpace(nome))
            {
                model.Alunos = BancoDeDados.Alunos.ToList();
                return View(model);
            }

            model.Alunos = BancoDeDados.Alunos.Where(aluno => aluno.Nome.Contains(nome)).ToList();
            model.Filtro = nome;

            return View(model);
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
            var viewModel = new Models.Alunos.CadastroModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Cadastro(string nome, string email)
        {
            var erros = VerificarErros(nome, email);

            if (erros.Count > 0)
            {
                var viewModel = new Models.Alunos.CadastroModel();
                viewModel.Errors = erros;
                return View(viewModel);
            }

            var aluno = new Aluno
            {
                Nome = nome,
                Email = email
            };

            BancoDeDados.Alunos.Add(aluno);
            BancoDeDados.SaveChanges();

            return Redirect("/alunos/index");
        }

        private List<string> VerificarErros(string nome, string email)
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(email))
                erros.Add("E-mail é obrigatório");

            if (string.IsNullOrWhiteSpace(nome))
                erros.Add("Nome é obrigatório");

            return erros;
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var aluno = BancoDeDados.Alunos.Find(id);

            return View(aluno);
        }
        
        [HttpPost]
        public ActionResult Editar(int id, string nome, string email)
        {
            var aluno = BancoDeDados.Alunos.Find(id);

            aluno.Nome = nome;
            aluno.Email = email;

            BancoDeDados.Alunos.Update(aluno);
            BancoDeDados.SaveChanges();

            return Redirect("/alunos");
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            var aluno = BancoDeDados.Alunos.Find(id);

            return View(aluno);
        }

        [HttpPost]
        public ActionResult ExcluirConfirmado(int id)
        {
            var aluno = BancoDeDados.Alunos.Find(id);

            if (aluno == null)
                return Redirect("/alunos");

            BancoDeDados.Alunos.Remove(aluno);
            BancoDeDados.SaveChanges();

            return Redirect("/alunos");
        }
    }
}
