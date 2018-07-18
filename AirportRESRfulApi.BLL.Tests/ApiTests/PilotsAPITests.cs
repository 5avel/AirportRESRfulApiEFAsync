using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace APITests
{
    [TestFixture]
    class PilotsAPITests
    {
        IWebDriver driver;

        public PilotsAPITests()
        {
            string d = AppContext.BaseDirectory;
            driver = new FirefoxDriver(AppContext.BaseDirectory+ "geckodriver");
        }


        [SetUp]
        public void Init()
        {

        }

        [TearDown]
        public void TestTearDown()
        {
            driver.Quit();
        }



        [Test]
        public void Test()
        {


            
            driver.Navigate().GoToUrl("http://localhost:5000/api/Tickets");
            driver.FindElement(By.Id("gbqfq")).SendKeys("AutoQA.org");
            driver.FindElement(By.Id("gbqfq")).SendKeys(Keys.Enter);
        }
    }
}
