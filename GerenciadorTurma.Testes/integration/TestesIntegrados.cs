using System;
using Xunit;
using GerenciadorTurma.Controllers;
using GerenciadorTurma.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GerenciadorTurma.Models;
using System.Linq;

namespace GerenciadorTurma.Testes
{
    [Trait("", "integration")]
    public class TestesIntegrados
    {
        [Fact]
        public void Teste_cadastrar_aluno()
        {
            //Given

            var nome = "vitor" + RandomString(10);
            var email = "vitor@teste.com" + RandomString(5);

            var options2 = new DbContextOptionsBuilder<BancoDeDados>()
                .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GerenciadorTurma;Integrated Security=True;Connect Timeout=5;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .Options; //unit

            var bancoDeDados = new BancoDeDados(options2);

            var controller = new AlunosController(bancoDeDados);

            //When 
            controller.Cadastro(nome, email);

            //Then
            var view = controller.Index("") as ViewResult;
            var alunosModel = view.Model as AlunosModel;

            var aluno = alunosModel.Alunos.Find(aluno => aluno.Nome == nome && aluno.Email == email);

            Assert.NotNull(aluno);
        }

        [Fact]
        public void Teste_remover_aluno_cadastrado_criando_aluno_antes()
        {
            //Given
            var options2 = new DbContextOptionsBuilder<BancoDeDados>()
                .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GerenciadorTurma;Integrated Security=True;Connect Timeout=5;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .Options; //unit

            var bancoDeDados = new BancoDeDados(options2);

            var controller = new AlunosController(bancoDeDados);

            controller.Cadastro("nome", "email@teste.com");
            var view = controller.Index("") as ViewResult;
            var alunosModel = view.Model as AlunosModel;

            var alunoId = alunosModel.Alunos.First().Id;

            ////When 
            var result = controller.ExcluirConfirmado(alunoId);
            var redirect = result as RedirectResult;

            ////Then
            Assert.Equal("/alunos", redirect.Url);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}
