using NUnit.Framework;
using OpenQA.Selenium;
using System.Security.Cryptography;

namespace AuthorizationTests
{
    public class Tests
    {   
        private IWebDriver driver;
        private readonly By _pricingLink = By.XPath("/html/body/div[2]/div/div[1]/a");
        private readonly By _singInButton = By.XPath("/html/body/div[2]/div/div[3]/a[1]");
        private readonly By _loginButton = By.XPath("/html/body/div[1]/div/div/div[2]/div[1]/form/div[2]/div/input");
        private readonly By _passwordInput = By.XPath("/html/body/div[1]/div/div/div[2]/div[1]/form/div[3]/div/input");
        private readonly By _singInButton2 = By.XPath("/html/body/div[1]/div/div/div[2]/div[1]/form/div[5]/button");

        private const string _login = "milhot";
        private const string _password = "sanya5555";

        [SetUp]
        public void Setup() //В методе под атрибутом SetUp происходит то, что будет перед              //в скобках прописываем инициализацию экземпляров (их вызов), переходим на веб-страницу.
        {
            driver = new OpenQA.Selenium.Edge.EdgeDriver();
            driver.Navigate().GoToUrl("https://lib.social/forum/?category=all&title&user_id&subscription=0&page=1&sort=newest");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        [Test]
        public void TestPageTitle()
        {
            Assert.That(driver.Title, Is.EqualTo("Форум"));
        }

        [Test]
        public void TestObjectVisibility()
        {
            var element = driver.FindElement(_singInButton);
            Assert.IsTrue(element.Displayed);
        }

        [Test]
        public void TestLinkNavigation()
        {
            var link = driver.FindElement(_pricingLink);
            Thread.Sleep(1000);
            link.Click();
            Thread.Sleep(1000);
            Assert.AreEqual("https://lib.social/", driver.Url);
        }


        [Test]
        public void TestLogin()
        {
            Thread.Sleep(2000);
            var singIn = driver.FindElement(_singInButton);
            singIn.Click();
            Thread.Sleep(1000);
            var login = driver.FindElement(_loginButton);
            login.SendKeys(_login);
            Thread.Sleep(1000);
            var password = driver.FindElement(_passwordInput);
            password.SendKeys(_password);

            Assert.AreEqual(_login, login.GetAttribute("value"));
            Assert.AreEqual(_password, password.GetAttribute("value"));

            Thread.Sleep(1000);
            var singIn2 = driver.FindElement(_singInButton2);
            singIn2.Click();
            Thread.Sleep(500);

            Assert.AreEqual("https://lib.social/?section=home-updates-7423652", driver.Url);

        }


        [TearDown]
        public void TearDown() //Метод TearDown вызывается после прохождения тестов. Здесь происходит закрытие веб-приложений
        {
           driver.Close();
        }


    }
}

