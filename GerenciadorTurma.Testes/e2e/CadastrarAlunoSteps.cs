using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace GerenciadorTurma.Testes.e2e
{
    [Binding]
    public class CadastrarAlunoSteps
    {
        private IWebDriver driver = new ChromeDriver();

        [Given(@"que acessei a página de cadastro de alunos")]
        public void DadoQueAcesseiAPaginaDeCadastroDeAlunos()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://localhost:5001/alunos/cadastro");

            //wait.Until(webDriver => webDriver.FindElement(By.CssSelector("h3")).Displayed);
            //IWebElement firstResult = driver.FindElement(By.CssSelector("h3"));
            //Console.WriteLine(firstResult.GetAttribute("textContent"));

        }

        [Given(@"informei o nome '(.*)'")]
        public void DadoInformeiONome(string nome)
        {
            driver.FindElement(By.Id("nome")).SendKeys(nome);
        }

        [Given(@"informei o email '(.*)'")]
        public void DadoInformeiOEmail(string email)
        {
            driver.FindElement(By.Id("email")).SendKeys(email);
        }

        [When(@"clicar no botão de cadastrar")]
        public void QuandoClicarNoBotaoDeCadastrar()
        {
            driver.FindElement(By.Id("cadastrar")).Click();
        }

        [Then(@"devo ser redirecionado para a página de lista de alunos")]
        public void EntaoDevoSerRedirecionadoParaAPaginaDeListaDeAlunos()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"um aluno com o nome '(.*)' e email '(.*)' deve existir na página")]
        public void EntaoUmAlunoComONomeEEmailDeveExistirNaPagina(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
