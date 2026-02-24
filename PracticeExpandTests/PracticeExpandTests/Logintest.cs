using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System.Threading;

namespace SeleniumLoginTests
{
    public class LoginTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void ValidLogin_ShouldShowSuccessMessage()
        {
            // otwarcie strony
            driver.Navigate().GoToUrl("https://practice.expandtesting.com/login");

            Thread.Sleep(2000);

            // pola logowania
            var username = driver.FindElement(By.Id("username"));
            var password = driver.FindElement(By.Id("password"));

            username.SendKeys("practice");
            password.SendKeys("SuperSecretPassword!");

            // przycisk login + scroll
            var loginBtn = driver.FindElement(By.CssSelector("button[type='submit']"));

            ((IJavaScriptExecutor)driver)
                .ExecuteScript("arguments[0].scrollIntoView(true);", loginBtn);

            Thread.Sleep(500);

            loginBtn.Click();

            Thread.Sleep(2000);

            // komunikat sukcesu
            var message = driver.FindElement(By.Id("flash"));

            Assert.That(message.Text,
                Does.Contain("You logged into a secure area!"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}