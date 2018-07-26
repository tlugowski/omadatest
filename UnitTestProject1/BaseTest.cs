using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestFixture]
    public class BaseTest
    {
        protected IWebDriver Driver { get; set; }

        [SetUp]
        public void Initalize()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void CloseTest()
        {
            String mainWindow = Driver.CurrentWindowHandle;
            Driver.WindowHandles.Where(w => w != mainWindow).ToList()
           .ForEach(w =>{Driver.SwitchTo().Window(w);
            Driver.Close();});
            Driver.SwitchTo().Window(mainWindow).Close();
        }
    }
}
