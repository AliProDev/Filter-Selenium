using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Filter
{
    [Binding]
    [Scope(Tag = "Filter")]
    public sealed class FilterStep
    {
        IWebDriver webDriver;
        FilterPage Filter;

        #region Launch_Application

        [Given(@"launch application")]
        public void Launch_Application()
        {
            webDriver = new ChromeDriver();
            webDriver.Navigate().GoToUrl("https://demos.telerik.com/kendo-ui/filter/custom-editors");
            Filter = new FilterPage(webDriver);
            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Cookies.DeleteAllCookies();
            System.Threading.Thread.Sleep(2000);
        }

        #endregion

        #region Filter_Section

        public class filter
        {
            public string Feild { get; set; }
            public string Operator { get; set; }
            public string Value { get; set; }
        }

        [When(@"add filter section and input information")]
        public void Filter_Section(Table table)
        {
            System.Threading.Thread.Sleep(2000);
            var info = table.CreateSet<filter>();
            foreach (filter item in info)
            {
                Filter.Filter_Section(item.Feild, item.Operator, item.Value);
            }
        }

        #endregion

        #region Check_Grid

        [Then(@"check grid information")]
        public void Check_Grid()
        {
            System.Threading.Thread.Sleep(2000);
            Filter.Check_Grid();
        }

        #endregion

    }
}
