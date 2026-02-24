using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Security.Cryptography.X509Certificates;


namespace Test1inputs
{
    [TestFixture]
    public class InputsTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            new WebDriverManager.DriverManager().SetUpDriver(
                new WebDriverManager.DriverConfigs.Impl.ChromeConfig());

            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void InputNumber_ShouldAcceptValue()
        {
            driver.Navigate().GoToUrl("https://practice.expandtesting.com/inputs");

            var input = driver.FindElement(By.Id("input-number"));
            input.SendKeys("123");

            Assert.That(input.GetAttribute("value"), Is.EqualTo("123"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}