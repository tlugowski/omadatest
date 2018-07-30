using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class Page
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        public string url = ConfigurationSettings.AppSettings["url"];
        byte timeWait = Convert.ToByte(ConfigurationSettings.AppSettings["timeWait"]);

        internal Page(IWebDriver Driver)
        {
            driver = Driver;
        }

        public Page(IWebDriver Driver, WebDriverWait wait) : this(Driver)
        {
            this.wait = wait;
        }

        public void WaitForAllScriptsLoaded()
        {
            var jsexe = (IJavaScriptExecutor)driver;
            bool watcher = false;
            while (watcher)
            {
                var status = jsexe.ExecuteScript("return document.readyState");
                if (status == "complete") { watcher = true; }
            }
        }

        public void WaitUntilTagAppear()
        {
            var select = wait.Until(ExpectedConditions.ElementExists(By.TagName("tag")));
        }
        public void WaitUntilElementClicable(string selector)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(selector)));
        }


        public void WaitForTextOnPage(String selector)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(d =>
            {
                try
                {
                    driver.FindElement(By.CssSelector(selector));
                }
                catch (NoSuchElementException e)
                {
                    return false;
                }
                return true;
            });
        }
        public void WaitForTextOnPageByClassName(String classname)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(classname)));
        }
        public void MoveTheSubmenu(string selector)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(selector)));
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
        }

        public void ClickOn(String element)
        {
            driver.FindElement(By.CssSelector(element)).Click();
        }
        public void RightClickOn(String selector)
        {
            Actions action = new Actions(driver);
            IWebElement element = driver.FindElement(By.CssSelector(selector));
            action.ContextClick(element).Perform();
        }

        public void ClickOnById(String element)
        {
            driver.FindElement(By.Id(element)).Click();
        }
        public void ClickByXpath(String element)
        {
            driver.FindElement(By.XPath(element)).Click();
        }

        public void ClearAndFillField(string field, string text)
        {
            ClearField(field);
            SendKeys(field, text);
        }

        public void FillField(string field, string text)
        {
            SendKeys(field, text);
        }

        public void ClearField(String field)
        {
            driver.FindElement(By.CssSelector(field)).Clear();
        }

        public void SendKeys(String keySelector, String keys)
        {
            driver.FindElement(By.CssSelector(keySelector)).SendKeys(keys);
        }
        public void NavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
        public void AssertPdaDateIsEqualTo(string expectedText, string cssSelector)
        {
            var fiu = driver.FindElement(By.CssSelector(cssSelector)).GetAttribute("value").ToString();
            Assert.AreEqual(expectedText, fiu);
        }

        public void AssertFieldTextIsEqualTo(string expectedText, string cssSelector)
        {
            var fiu = driver.FindElement(By.CssSelector(cssSelector));
            Assert.AreEqual(expectedText, fiu.Text);
        }
        public void CheckIfContainsText(string expectedText, string cssSelector)
        {
            string text = driver.FindElement(By.CssSelector(cssSelector)).Text;
            if (text.Contains(expectedText)) { }
            else
            {
                throw new Exception("Actual text doesn't match with expected.");
            }
        }

        public void SelectElement(string elementSelector, string text)
        {
            new SelectElement(driver.FindElement(By.CssSelector(elementSelector))).SelectByText(text);
        }
        public void Submit()
        {
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
        }
        public void WindowsSenKeys(string text)
        {
            System.Windows.Forms.SendKeys.SendWait(text);
        }

        public void PageGoDown(int downStep)
        {
            for (int i = 1; i < downStep; i++)
            {
                System.Windows.Forms.SendKeys.SendWait("{DOWN}");
            }
        }
        public void SwitchToWindow(Expression<Func<IWebDriver, bool>> predicateExp)
        {
            var predicate = predicateExp.Compile();
            foreach (var handle in driver.WindowHandles)
            {
                driver.SwitchTo().Window(handle);
                if (predicate(driver))
                {
                    return;
                }
            }

            throw new ArgumentException(string.Format("Unable to find window with condition: '{0}'", predicateExp.Body));
        }
        public void CheckIsElementNotDisplayed(string notExpectedObject)
        {
            Assert.AreEqual(false, driver.FindElement(By.CssSelector(notExpectedObject)).Displayed);
        }

        public void ClickByPartialHref(string partialHref)
        {
            driver.FindElement(By.XPath("//a[contains(@href,'" + partialHref + "')]")).Click();
        }
        public void SelectElementEnabled(string elementselector, string text)
        {
            var zmienna = driver.FindElement(By.CssSelector(elementselector));
            if (zmienna != null)
            {
                IWebElement element = zmienna;
                if (element.Displayed && element.Enabled)
                {
                    SelectElement(elementselector, text);
                }
            }
        }
        public void SetCheckboxChecked(string elementSelector, bool isChecked)
        {
            if (GetCheckboxChecked(elementSelector) != isChecked)
            {
                driver.FindElement(By.CssSelector(elementSelector)).Click();
            }
        }
        public bool GetCheckboxChecked(string elementSelector)
        {
            return driver.FindElement(By.CssSelector(elementSelector)).Selected;
        }
        public void SlideSlider(string Selector)
        {
            IWebElement slider = driver.FindElement(By.CssSelector(Selector));
            Actions action = new Actions(driver);
            action.ClickAndHold(slider);
            System.Threading.Thread.Sleep(500);
            action.MoveByOffset(100, 0).Perform();
            System.Threading.Thread.Sleep(500);
            action.Release().Build().Perform();
        }

    }
}
