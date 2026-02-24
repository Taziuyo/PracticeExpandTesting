using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestyAutomatyczne.E2E.Tests
{
    public class RegisterTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void CorrectRegisterTest()
        {
            driver.Navigate().GoToUrl("https://practice.expandtesting.com/register");

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")));

            string randomUsername = "testuser" + DateTime.Now.Ticks;

            driver.FindElement(By.Id("username")).SendKeys(randomUsername);
            driver.FindElement(By.Id("password")).SendKeys("Test123!");
            driver.FindElement(By.Id("confirmPassword")).SendKeys("Test123!");

            IWebElement registerButton =
                wait.Until(ExpectedConditions.ElementIsVisible(
                    By.CssSelector("button[type='submit']")
                ));

            //  scroll do przycisku
            ((IJavaScriptExecutor)driver)
                .ExecuteScript("arguments[0].scrollIntoView(true);", registerButton);

            //  BEZPIECZNY KLIK (eliminuje ElementClickInterceptedException)
            ((IJavaScriptExecutor)driver)
                .ExecuteScript("arguments[0].click();", registerButton);

            //  czekamy na redirect
            wait.Until(ExpectedConditions.UrlContains("/login"));

            IWebElement successMessage = wait.Until(
                ExpectedConditions.ElementIsVisible(By.Id("flash"))
            );

            Assert.That(successMessage.Text,
                        Does.Contain("Successfully registered")
                );
        }

    }
}