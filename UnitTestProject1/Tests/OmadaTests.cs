using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;


namespace UnitTestProject1
{
    [TestFixture]
    public class OmadaTests : BaseTest
    {


        [TestCategory("FireFox")]
        [Test]
        public void OmadaPageTest()
        {
            var test = new FirstPage(Driver);
            test.GoToPage();
            test.ExecuteSearch();
            test.GoToLinkAndVerifyResult();
            test.NavigateToNewsAndVerifyActualNews();
            test.GoToHomePageAndChangeLocation();
            test.OpenPageInNewTab();
            test.MakeSurePrivacyPolicyNotAppearAgain();
            test.FillDataAndDownloadPDF();
        }







    }
}
