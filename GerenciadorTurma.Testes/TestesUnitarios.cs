using System;
using Xunit;
using GerenciadorTurma.Controllers;
using GerenciadorTurma.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GerenciadorTurma.Models;
using System.Linq;
using GerenciadorTurma.Models.Alunos;

namespace GerenciadorTurma.Testes
{
    public class TestesUnitarios
    {
        [Fact]
        public void Teste_cadastrar_aluno()
        {
            //Given
            var nome = "vitor";
            var email = "vitor@teste.com";
            var controller = CriarControllerParaTeste();

            //When 
            controller.Cadastro(nome, email);

            //Then
            var view = controller.Index("") as ViewResult;
            var alunosModel = view.Model as AlunosModel;

            var aluno = alunosModel.Alunos.First();

            Assert.Equal("vitor", aluno.Nome);
            Assert.Equal("vitor@teste.com", aluno.Email);
        }

        [Fact]
        public void Teste_cadastrar_aluno_sem_informar_email_deve_falhar()
        {
            //Given
            var nome = "vitor";
            var email = "";

            var controller = CriarControllerParaTeste();

            ////When 
            var view = controller.Cadastro(nome, email) as ViewResult;
            var model = view.Model as CadastroModel;

            ////Then
            Assert.Single(model.Errors, "E-mail é obrigatório");
        }

        [Fact]
        public void Teste_cadastrar_aluno_sem_informar_nome_deve_falhar()
        {
            //Given
            var nome = "";
            var email = "vitor@teste.com";
            var controller = CriarControllerParaTeste();

            ////When 
            var view = controller.Cadastro(nome, email) as ViewResult;
            var model = view.Model as CadastroModel;

            ////Then
            Assert.Single(model.Errors, "Nome é obrigatório");
        }

        [Fact]
        public void Teste_cadastrar_aluno_sem_informar_nome_e_email_deve_falhar()
        {
            //Given
            var nome = "";
            var email = "";

            var controller = CriarControllerParaTeste();

            ////When 
            var view = controller.Cadastro(nome, email) as ViewResult;
            var model = view.Model as CadastroModel;

            ////Then
            Assert.Single(model.Errors, "Nome é obrigatório");
            Assert.Single(model.Errors, "E-mail é obrigatório");
        }

        public AlunosController CriarControllerParaTeste()
        {
            var options2 = new DbContextOptionsBuilder<BancoDeDados>()
                    .UseInMemoryDatabase("Teste")
                    .Options;

            var bancoDeDados = new BancoDeDados(options2);

            return new AlunosController(bancoDeDados);
        }
    }

    //public class TestesIntegrados
    //{
    //    [Fact]
    //    public void Teste_cadastrar_aluno()
    //    {
    //        //Given
    //        var nome = "vitor"; ;
    //        var email = "vitor@teste.com";

    //        var options2 = new DbContextOptionsBuilder<BancoDeDados>()
    //            .UseSqlServer(@"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GerenciadorTurma;Integrated Security=True;Connect Timeout=5;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    //            .Options; //unit

    //        var bancoDeDados = new BancoDeDados(options2);

    //        var controller = new AlunosController(bancoDeDados);

    //        //When 
    //        controller.Cadastro(nome, email);

    //        //Then
    //        var view = controller.Index("") as ViewResult;
    //        var alunosModel = view.Model as AlunosModel;

    //        var aluno = alunosModel.Alunos.First();

    //        Assert.Equal("vitor", aluno.Nome);
    //        Assert.Equal("vitor@teste.com", aluno.Email);
    //    }
    //}
}
