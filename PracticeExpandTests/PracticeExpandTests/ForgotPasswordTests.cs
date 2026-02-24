using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestyAutomatyczne.E2E.Tests
{
    public class ForgotPasswordTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            new WebDriverManager.DriverManager().SetUpDriver(
                new WebDriverManager.DriverConfigs.Impl.ChromeConfig());

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void CorrectForgotPasswordTest()
        {
            driver.Navigate().GoToUrl("https://practice.expandtesting.com/forgot-password");

            IWebElement emailInput = driver.FindElement(By.Id("email"));
            emailInput.SendKeys("test@email.com");

            IWebElement retrieveButton = driver.FindElement(By.CssSelector("button[type='submit']"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].scrollIntoView(true);", retrieveButton);

            Thread.Sleep(1000);

            retrieveButton.Click();

            // sprawdzenie, czy nastąpiło przejście na stronę potwierdzenia
            string pageTitle = driver.Title;

            Assert.That(
                pageTitle,
                Does.Contain("Email sent")
            );
        }
    }
}