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
        [Test]
        public void Test()
        {
            
            IWebDriver driver = new FirefoxDriver(AppContext.BaseDirectory);
            driver.Url = "http://www.demoqa.com";
        }
    }
}
