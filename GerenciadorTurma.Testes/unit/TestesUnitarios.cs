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
    [Trait("","unit")]
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

        [Fact]
        public void Teste_remover_aluno_cadastrado_criando_aluno_antes()
        {
            //Given
            var controller = CriarControllerParaTeste();

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

        [Fact]
        public void Teste_remover_aluno_sem_criar_o_aluno()
        {
            //Given
            var identificador = 123;

            var controller = CriarControllerParaTeste();

            ////When 
            var result = controller.ExcluirConfirmado(identificador);
            var redirect = result as RedirectResult;

            ////Then
            Assert.Equal("/alunos", redirect.Url);
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

}
