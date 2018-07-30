﻿using System;
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
        string css_MoreBtn = "#navigation>ul>li.header__menuitem--megamenu.js-menuitem.is-right-aligned.has-submenu>a";
        string css_NextInMoreBtn = "li.header__menuitem--megamenu:nth-child(5)>div:nth-child(3)>ul:nth-child(1)>div:nth-child(5)>div:nth-child(2)>a:nth-child(1)";
        string css_OmadaNews = "#brick-701>div>h1";
        string css_HomeLogo = "body>header>div.header__container>div>div.header__column--home>a>img";
        string css_SecurityLogo = "#content>div.brick.spots--variant4>div>div>div:nth-child(1)>section>img";
        string css_ContactBtn = "body>header>div.header__function-nav>div.header__function-nav--right>ul>li:nth-child(1)>a";
        string css_UswestBtn = "#brick-3475>div>div>div.tabmenu__menu>span:nth-child(3)";
        string css_UnitedStatesWestText = "#brick-3475>div>div>div.tabmenu__tabs>div.tabmenu__tab.selected>div>section>div>div>div:nth-child(1)>div>span";
        string classBottomActions = "bottom-actions";
        string css_GermanyBtn = "#brick-3475>div>div>div.tabmenu__menu>span:nth-child(4)";
        string css_ReadPrivacyPolicy = ".cookiebar__read-more";
        string css_WebsitePrivacyPolicyText = "#brick-10308>div>div>div>section>h1";
        string css_ReadPrivacyCloseBtn = ".cookiebar__button";
        string css_CasesBtn = "#navigation>ul>li.header__menuitem--megamenu.js-menuitem.is-right-aligned.is-selected.has-submenu>div>ul>div:nth-child(3)>div:nth-child(2)>a";
        string css_OmadaCusotersText = "#brick-1765>div>h1";
        string eccoPartialHref = "/en-us/more/customers/cases/ecco-case";
        string css_EccoLogo = "#brick-1595>div>h1";
        string css_DownloadPDFBtn = "#btnSubmit";
        string css_JobTitle = "#f_84ef53b4f80ee71180eac4346bac2ebc";
        string css_FirstName = "#f_7bb79f2f2aa5e61180e4c4346bac7e3c";
        string css_LastName = "#f_501281762aa5e61180e4c4346bac7e3c";
        string css_Email = "#f_511a8b932aa5e61180e4c4346bac7e3c";
        string css_BusinessPhone = "#f_5d3af1ac19a8e61180dfc4346bad20a4";
        string css_CompanyName = "#f_42b109e9c1a5e61180e4c4346bac7e3c";
        string css_Country = "#f_61d4da016ca6e61180dfc4346bad20a4";
        string checkBoxAccepted = "#f_3557f512c2a5e61180e4c4346bac7e3c";
        string css_Slider = "#Slider";
        string css_ThanksForDownloadedPDF = "#brick-1600>div>div>div>section>h1";
        string css_DownloadLink = "#brick-1600>div>div>div>section>div>p>a";
        string css_copyright = "#brick-20>div.footer__container--bottom";

        internal void FillDataAndDownloadPDF()
        {
            NavigateToCases();
            ClickEccoCompanyDownloadBtn();
            PageGoDown(24);
            WaitForTextOnPage(css_DownloadPDFBtn);
            FillForm();
            PageGoDown(6);
            WaitForTextOnPage(css_Slider);
            SlideSlider(css_Slider);
            ClickOn(css_DownloadPDFBtn);
            WaitForTextOnPage(css_ThanksForDownloadedPDF);
            ClickOn(css_DownloadLink);
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
            ClearAndFillField(css_JobTitle, ConfigurationManager.AppSettings["Job"]);
            ClearAndFillField(css_FirstName, ConfigurationManager.AppSettings["Name"]);
            ClearAndFillField(css_LastName, ConfigurationManager.AppSettings["LastName"]);
            ClearAndFillField(css_Email, ConfigurationManager.AppSettings["Email"]);
            ClearAndFillField(css_BusinessPhone, ConfigurationManager.AppSettings["PhoneNumber"]);
            ClearAndFillField(css_CompanyName, ConfigurationManager.AppSettings["CompanyName"]);
            SelectElementEnabled(css_Country, ConfigurationManager.AppSettings["Country"]);
            SetCheckboxChecked(checkBoxAccepted, true);
        }
    
        private void ClickEccoCompanyDownloadBtn()
        {
            ClickByPartialHref(eccoPartialHref);
            WaitForTextOnPage(css_EccoLogo);
            WaitForAllScriptsLoaded();
        }

        private void NavigateToCases()
        {
            MoveTheSubmenu(css_MoreBtn);
            WaitForTextOnPage(css_CasesBtn);
            WaitUntilElementClicable(css_CasesBtn);
            ClickOn(css_CasesBtn);
            WaitForTextOnPage(css_OmadaCusotersText);
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
            WaitUntilElementClicable(css_GermanyBtn);
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
            var locationGermanyButton = driver.FindElement(By.CssSelector(css_GermanyBtn)).Location;
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
            ClickOn(css_UswestBtn);
            PageGoDown(5);
            WaitForTextOnPage(css_UnitedStatesWestText);
            WaitForAllScriptsLoaded();
            CaptureGlobalScreenAndSave(ConfigurationManager.AppSettings["FirstScreenPath"]);
        }


        private void GoToContact()
        {
            ClickOn(css_ContactBtn);
            WaitForTextOnPage(css_UswestBtn);
            WaitForAllScriptsLoaded();
        }

        private void GoToHomePage()
        {
            ClickOn(css_HomeLogo);
            WaitForTextOnPage(css_SecurityLogo);
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
            MoveTheSubmenu(css_MoreBtn);
            WaitForTextOnPage(css_NextInMoreBtn);
            WaitUntilElementClicable(css_NextInMoreBtn);
            ClickOn(css_NextInMoreBtn);
            WaitForTextOnPage(css_OmadaNews);
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
            WaitForTextOnPage(css_ReadPrivacyPolicy);
            RightClickOn(css_ReadPrivacyPolicy);
            System.Threading.Thread.Sleep(500);
            WindowsSenKeys("{DOWN}{ENTER}");
            System.Threading.Thread.Sleep(4000);
            SwitchToWindow(driver => driver.Title == "Omada | Processing of Personal Data");
            WaitForTextOnPage(css_WebsitePrivacyPolicyText);
            WaitForAllScriptsLoaded();
        }

        internal void MakeSurePrivacyPolicyNotAppearAgain()
        {
            SwitchToWindow(driver => driver.Title == "Contact Omada | Leading Provider of IT Security Solutions");
            WaitForAllScriptsLoaded();
            WaitForTextOnPage(css_ReadPrivacyCloseBtn);
            ClickOn(css_ReadPrivacyCloseBtn);
            ReflashThePage();
            WaitForAllScriptsLoaded();
            CheckIsElementNotDisplayed(css_ReadPrivacyCloseBtn);
            System.Threading.Thread.Sleep(4000);
        }

        private void ReflashThePage()
        {
            WindowsSenKeys("{F5}");
        }



    }
}
