using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Diagnostics;
using OpenQA.Selenium.Support.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Web.UI.WebControls;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using System.Globalization;
using System.Windows.Forms;
using UnitTestProject1.Utils;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Configuration;

namespace UnitTestProject1.Pages
{
    public class FirstPage : Page
    {
        public FirstPage(IWebDriver driver) : base(driver)
        {
        }

       
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_MOVE = 0x001;

        string actualTime = DateTime.Now.ToString("MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
        string CSS_MoreBtn = "#navigation>ul>li.header__menuitem--megamenu.js-menuitem.is-right-aligned.has-submenu>a";
        string CSS_NextInMoreBtn = "li.header__menuitem--megamenu:nth-child(5)>div:nth-child(3)>ul:nth-child(1)>div:nth-child(5)>div:nth-child(2)>a:nth-child(1)";
        string CSS_OmadaNews = "#brick-701>div>h1";
        string CSS_HomeLogo = "body>header>div.header__container>div>div.header__column--home>a>img";
        string CSS_SecurityLogo = "#content>div.brick.spots--variant4>div>div>div:nth-child(1)>section>img";
        string CSS_ContactBtn = "body>header>div.header__function-nav>div.header__function-nav--right>ul>li:nth-child(1)>a";
        string CSS_UswestBtn = "#brick-3475>div>div>div.tabmenu__menu>span:nth-child(3)";
        string CSS_UnitedStatesWestText = "#brick-3475>div>div>div.tabmenu__tabs>div.tabmenu__tab.selected>div>section>div>div>div:nth-child(1)>div>span";
        string ClassBottomActions = "bottom-actions";
        string CSS_GermanyBtn = "#brick-3475>div>div>div.tabmenu__menu>span:nth-child(4)";
        string CSS_ReadPrivacyPolicy = ".cookiebar__read-more";
        string CSS_WebsitePrivacyPolicyText = "#brick-10308>div>div>div>section>h1";
        string CSS_ReadPrivacyCloseBtn = ".cookiebar__button";
        string CSS_CasesBtn = "#navigation>ul>li.header__menuitem--megamenu.js-menuitem.is-right-aligned.is-selected.has-submenu>div>ul>div:nth-child(3)>div:nth-child(2)>a";
        string CSS_OmadaCusotersText = "#brick-1765>div>h1";
        string eccoPartialHref = "/en-us/more/customers/cases/ecco-case";
        string CSS_EccoLogo = "#brick-1595>div>h1";
        string CSS_DownloadPDFBtn = "#btnSubmit";
        string CSS_JobTitle = "#f_84ef53b4f80ee71180eac4346bac2ebc";
        string CSS_FirstName = "#f_7bb79f2f2aa5e61180e4c4346bac7e3c";
        string CSS_LastName = "#f_501281762aa5e61180e4c4346bac7e3c";
        string CSS_Email = "#f_511a8b932aa5e61180e4c4346bac7e3c";
        string CSS_BusinessPhone = "#f_5d3af1ac19a8e61180dfc4346bad20a4";
        string CSS_CompanyName = "#f_42b109e9c1a5e61180e4c4346bac7e3c";
        string CSS_Country = "#f_61d4da016ca6e61180dfc4346bad20a4";
        string checkBoxAccepted = "#f_3557f512c2a5e61180e4c4346bac7e3c";
        string CSS_Slider = "#Slider";
        string CSS_ThanksForDownloadedPDF = "#brick-1600>div>div>div>section>h1";
        string CSS_DownloadLink = "#brick-1600>div>div>div>section>div>p>a";
        string css_copyright = "#brick-20>div.footer__container--bottom";

        internal void FillDataAndDownloadPDF()
        {
            NavigateToCases();
            ClickEccoCompanyDownloadBtn();
            PageGoDown(24);
            WaitForTextOnPage(CSS_DownloadPDFBtn);
            FillForm();
            PageGoDown(6);
            WaitForTextOnPage(CSS_Slider);
            SlideSlider(CSS_Slider);
            ClickOn(CSS_DownloadPDFBtn);
            WaitForTextOnPage(CSS_ThanksForDownloadedPDF);
            ClickOn(CSS_DownloadLink);
            CheckIfFileDownloaded();
        }

        private bool CheckIfFileDownloaded()
        {
            var user = Environment.ExpandEnvironmentVariables("%userprofile%").Replace("C:\\Users\\", "");
            var destiny = @"C:\Users\" + user + @"\Downloads\" + "Omada-Case-ECCO-Shoes.pdf";
            if (File.Exists(destiny))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void FillForm()
        {
            ClearAndFillField(CSS_JobTitle, ConfigurationManager.AppSettings["Job"]);
            ClearAndFillField(CSS_FirstName, ConfigurationManager.AppSettings["Name"]);
            ClearAndFillField(CSS_LastName, ConfigurationManager.AppSettings["LastName"]);
            ClearAndFillField(CSS_Email, ConfigurationManager.AppSettings["Email"]);
            ClearAndFillField(CSS_BusinessPhone, ConfigurationManager.AppSettings["PhoneNumber"]);
            ClearAndFillField(CSS_CompanyName, ConfigurationManager.AppSettings["CompanyName"]);
            SelectElementEnabled(CSS_Country, ConfigurationManager.AppSettings["Country"]);
            SetCheckboxChecked(checkBoxAccepted, true);
        }
    
        private void ClickEccoCompanyDownloadBtn()
        {
            ClickByPartialHref(eccoPartialHref);
            WaitForTextOnPage(CSS_EccoLogo);
            WaitForAllScriptsLoaded();
        }

        private void NavigateToCases()
        {
            MoveTheSubmenu(CSS_MoreBtn);
            WaitForTextOnPage(CSS_CasesBtn);
            WaitUntilElementClicable(CSS_CasesBtn);
            ClickOn(CSS_CasesBtn);
            WaitForTextOnPage(CSS_OmadaCusotersText);
        }

        internal void GoToHomePageAndChangeLocation()
        {
            GoToHomePage();
            GoToContact();
            GoToUsWest();
            ChangeMapLocationAndTakeScreenshots();
        }

        private void ChangeMapLocationAndTakeScreenshots()
        {
            WaitUntilElementClicable(CSS_GermanyBtn);
            int startYLocationInMap, oldXLocation, newXLocation, newYLocation;
            NewMethod(out startYLocationInMap, out oldXLocation, out newXLocation, out newYLocation);
            GoToFirstLocation(startYLocationInMap, oldXLocation);
            WaitForAllScriptsLoaded();
            CaptureGlobalScreenAndSave(ConfigurationManager.AppSettings["FirstMapPositionPath"]);
            GoToSecondLocation(newXLocation, newYLocation);
            WaitForAllScriptsLoaded();
            CaptureGlobalScreenAndSave(ConfigurationManager.AppSettings["SecondtMapPositionPath"]);
        }

        private void NewMethod(out int startYLocationInMap, out int oldXLocation, out int newXLocation, out int newYLocation)
        {
            var locationGermanyButton = driver.FindElement(By.CssSelector(CSS_GermanyBtn)).Location;
            startYLocationInMap = locationGermanyButton.Y = locationGermanyButton.Y + 300;
            oldXLocation = locationGermanyButton.X;
            newXLocation = oldXLocation + 30;
            newYLocation = startYLocationInMap + 10;
        }

        private void GoToSecondLocation(int newXLocation, int newYLocation)
        {
            MouseMove(newXLocation, newYLocation);
            GoToPositionOnPage(newXLocation, newYLocation);
            LeftMouseClickUp(newXLocation, newYLocation);
        }

        private void GoToFirstLocation(int startYLocationInMap, int oldXLocation)
        {
            GoToPositionOnPage(oldXLocation, startYLocationInMap);
            LeftMouseClickDown(oldXLocation, startYLocationInMap);
        }

        private void GoToUsWest()
        {
            ClickOn(CSS_UswestBtn);
            PageGoDown(5);
            WaitForTextOnPage(CSS_UnitedStatesWestText);
            WaitForAllScriptsLoaded();
            CaptureGlobalScreenAndSave(ConfigurationManager.AppSettings["FirstScreenPath"]);
        }


        private void GoToContact()
        {
            ClickOn(CSS_ContactBtn);
            WaitForTextOnPage(CSS_UswestBtn);
            WaitForAllScriptsLoaded();
        }

        private void GoToHomePage()
        {
            ClickOn(CSS_HomeLogo);
            WaitForTextOnPage(CSS_SecurityLogo);
            WaitForAllScriptsLoaded();
        }

        internal void GoToLinkAndVerifyResult()
        {
            PageGoDown(11);
            WaitUntilElementClicable("#brick-411>div>div>section:nth-child(5)>a");
            WaitForTextOnPage("#brick-411>div>div>section:nth-child(5)>a");
            ClickOn("#brick-411>div>div>section:nth-child(5)>a");//link
            PageGoDown(10);
            WaitForTextOnPage("#brick-1502>div>div>div>section>div>h3");
            WaitForAllScriptsLoaded();
            CheckIfContainsText("Hot topics to be covered at Gartner's IAM Summit:", "#brick-1502>div>div>div>section>div>h3");
            WaitForAllScriptsLoaded();
        }

        internal void NavigateToNewsAndVerifyActualNews()
        {
            NavigateToNews();
            VerifyIfArticleIsOnPresentTime();
        }
        private void VerifyIfArticleIsOnPresentTime()
        {
            CheckIfTableContainsText();
        }
        private void NavigateToNews()
        {
            MoveTheSubmenu(CSS_MoreBtn);
            WaitForTextOnPage(CSS_NextInMoreBtn);
            WaitUntilElementClicable(CSS_NextInMoreBtn);
            ClickOn(CSS_NextInMoreBtn);
            WaitForTextOnPage(CSS_OmadaNews);
        }

        

        public void GoToPage()
        {
            NavigateTo(ConfigurationManager.AppSettings["url"]);
            WaitForAllScriptsLoaded();
            WaitForTextOnPage(css_copyright);
        }

        internal void ExecuteSearch()
        {
            ClearAndFillField(".header__search>input:nth-child(1)", "gartner");
            Submit();
            CheckTableSearchingResult();
        }
        public void CheckTableSearchingResult()
        {
            Assert.IsTrue(CheckIfTableReturnExpectedRows());
        }
        private bool CheckIfTableReturnExpectedRows()
        {
            WaitForTextOnPage(".breadcrumbs__menuitem > span:nth-child(1)");
            IWebElement tableElement = driver.FindElement(By.CssSelector(".search-results__content"));
            IList<IWebElement> tableRow = tableElement.FindElements(By.ClassName("search-results__item"));
            if (tableRow.Count < 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool CheckIfTableContainsText()
        {
            IWebElement tableElement = driver.FindElement(By.CssSelector("#content>div:nth-child(3)>div>div"));
            IList<IWebElement> tableRow = tableElement.FindElements(By.ClassName("cases__item"));
            if (tableRow.ToList().Any(span => span.Text.Contains(actualTime)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CaptureGlobalScreenAndSave(string savepath)
        {
            var image = ScreenCapture.CaptureActiveWindow();
            image.Save(savepath, ImageFormat.Jpeg);
        }
        public void GoToPositionOnPage(int x,int y)
        {
            Win32.POINT p = new Win32.POINT();
            Win32.SetCursorPos(x, y);
            Actions action = new Actions(driver);
        }

        public void LeftMouseClickDown(int xposDown, int yposDown)
        {
            SetCursorPos(xposDown, yposDown);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xposDown, yposDown, 0, 0);
            System.Threading.Thread.Sleep(1000);
        }
        public void LeftMouseClickUp(int xposUp, int yposUp)
        {
            SetCursorPos(xposUp, yposUp);
            mouse_event(MOUSEEVENTF_LEFTUP, xposUp, yposUp, 0, 0);
            System.Threading.Thread.Sleep(1000);
        }
        public void MouseMove(int xposUp, int yposUp)
        {
            mouse_event(MOUSEEVENTF_MOVE, xposUp, yposUp, 0, 0);
            System.Threading.Thread.Sleep(1000);
        }

        private const int MOUSEEVENTF_WHEEL = 0x0800;
        public void MouseWhileDown()
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(4000);
        }

        internal void OpenPageInNewTab()
        {
            WaitForTextOnPage(CSS_ReadPrivacyPolicy);
            RightClickOn(CSS_ReadPrivacyPolicy);
            System.Threading.Thread.Sleep(500);
            WindowsSenKeys("{DOWN}{ENTER}");
            System.Threading.Thread.Sleep(4000);
            SwitchToWindow(driver => driver.Title == "Omada | Processing of Personal Data");
            WaitForTextOnPage(CSS_WebsitePrivacyPolicyText);
            WaitForAllScriptsLoaded();
        }

        internal void MakeSurePrivacyPolicyNotAppearAgain()
        {
            SwitchToWindow(driver => driver.Title == "Contact Omada | Leading Provider of IT Security Solutions");
            WaitForAllScriptsLoaded();
            WaitForTextOnPage(CSS_ReadPrivacyCloseBtn);
            ClickOn(CSS_ReadPrivacyCloseBtn);
            ReflashThePage();
            WaitForAllScriptsLoaded();
            CheckIsElementNotDisplayed(CSS_ReadPrivacyCloseBtn);
            System.Threading.Thread.Sleep(4000);
        }

        private void ReflashThePage()
        {
            WindowsSenKeys("{F5}");
        }



    }
}
