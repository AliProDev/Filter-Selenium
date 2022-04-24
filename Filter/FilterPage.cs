using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
    class FilterPage
    {
        public IWebDriver WebDriver { get; }
        public FilterPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        #region List Information

        public class Item
        {
            public string ProductName { get; set; }
        }

        #endregion

        #region Get_Element

        //filter section
        public IWebElement btnaddfilter => WebDriver.FindElement(By.XPath("//div[@class='k-toolbar']/child::div[2]/child::button"));
        public IWebElement FeildFilter => WebDriver.FindElement(By.XPath("//div[@ul='k-filter-lines']/child::li/child::div/child::div/child::div[1]/child::span"));
        public IList<IWebElement> FeildList => WebDriver.FindElements(By.XPath("//div[@ul='k-filter-lines']/child::li/child::div/child::div/child::div[1]/child::span/child::select/child::option"));

        string Feilditem = "//div[@ul='k-filter-lines']/child::li/child::div/child::div/child::div[1]/child::span/child::select/child::option[{0}]";
        public IWebElement OperatorFilter => WebDriver.FindElement(By.XPath("//div[@ul='k-filter-lines']/child::li/child::div/child::div/child::div[2]/child::span"));
        public IList<IWebElement> OperatorList => WebDriver.FindElements(By.XPath("//div[@ul='k-filter-lines']/child::li/child::div/child::div/child::div[2]/child::span/child::select/child::option"));

        string Operatoritem = "//div[@ul='k-filter-lines']/child::li/child::div/child::div/child::div[2]/child::span/child::select/child::option[{0}]";
        public IWebElement ValueFilter => WebDriver.FindElement(By.Id("b74b9172-dcf9-4170-9feb-56b73aadb99f"));
        public IWebElement ApplyFilter => WebDriver.FindElement(By.XPath("//div[@ul='filter']/child::button"));

        //grid information
        public IList<IWebElement> gridinfo => WebDriver.FindElements(By.XPath("//div[@class='k-grid-content k-auto-scrollable']/child::table/child::tbody/child::tr"));

        string xpathgridinformation = "//div[@class='k-grid-content k-auto-scrollable']/child::table/child::tbody/child::tr[{0}]/child::td[{1}]";


        #endregion

        #region Filter_Section

        public void Filter_Section(string Feild, string Operator, string Value)
        {
            btnaddfilter.Click();
            System.Threading.Thread.Sleep(500);

            FeildFilter.Click();
            System.Threading.Thread.Sleep(500);
            var m = 1;
            foreach (var item in FeildList)
            {
                var feild = WebDriver.FindElement(By.XPath(String.Format(Feilditem, m))).Text;

                string feildconvert = feild.Replace(" ", "");
                string Feildconvert = Feild.Replace(" ", "");

                if (feildconvert == Feildconvert)
                {
                    WebDriver.FindElement(By.XPath(String.Format(Feilditem, m))).Click();
                    break;
                }

                m++;
            }
            System.Threading.Thread.Sleep(500);

            OperatorFilter.Click();
            System.Threading.Thread.Sleep(500);
            var l = 1;
            foreach (var item in OperatorList)
            {
                var operatorName = WebDriver.FindElement(By.XPath(String.Format(Operatoritem, l))).Text;

                string operatorNameconvert = operatorName.Replace(" ", "");
                string Operatorconvert = Operator.Replace(" ", "");

                if (operatorNameconvert == Operatorconvert)
                {
                    WebDriver.FindElement(By.XPath(String.Format(Operatoritem, l))).Click();
                    break;
                }

                l++;
            }
            System.Threading.Thread.Sleep(500);

            ValueFilter.SendKeys(Value);
            System.Threading.Thread.Sleep(500);

            ApplyFilter.Click();
            System.Threading.Thread.Sleep(1000);

        }

        #endregion

        #region Check_Grid

        public void Check_Grid()
        {
            var i = 0;
            var ListInformation = new List<Item>();

            foreach (var item in gridinfo)
            {
                var productname = "";
                productname = WebDriver.FindElement(By.XPath(String.Format(xpathgridinformation, i, 1))).Text;

                ListInformation.Add(new Item()
                {
                    ProductName = productname
                });

                System.Threading.Thread.Sleep(500);
                i++;
            }

            foreach (var item in ListInformation)
            {
                var value = ValueFilter.Text.Replace(" ", "");
                var productname = item.ProductName.Replace(" ", "");

                if (productname != value)
                {
                    Console.WriteLine("The name of the filtered product is not the same as the grade information");
                    Assert.Fail("The name of the filtered product is not the same as the grade information");
                }
            }

        }

        #endregion

    }
}
