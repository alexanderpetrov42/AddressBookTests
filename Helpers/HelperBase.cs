using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

namespace AddressBook
{
    public class HelperBase
    {
        public ApplicationManager manager;
        protected IWebDriver driver;
        protected WebDriverWait wait;
        private LoginPage loginPage;
        private ContactPage contactPage;
        private GroupPage groupPage;

        #region Pages
        public LoginPage LoginPage
        {
            get
            {
                if (loginPage == null)
                {
                    loginPage = new LoginPage();
                }

                return loginPage;
            }
        }

        public ContactPage ContactPage
        {
            get
            {
                if (contactPage == null)
                {
                    contactPage = new ContactPage();
                }

                return contactPage;
            }
        }

        public GroupPage GroupPage
        {
            get
            {
                if (groupPage == null)
                {
                    groupPage = new GroupPage();
                }

                return groupPage;
            }
        }
        #endregion

        public HelperBase(ApplicationManager manager)
        {
            driver = manager.driver;
            this.manager = manager;
        }

        public static Func<IWebDriver, IWebElement> Condition(By locator)
        {
            return (driver) =>
            {
                var element = driver.FindElements(locator).FirstOrDefault();
                return element != null && element.Displayed && element.Enabled ? element : null;
            };
        }

        public void Click(By locator)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(Condition(locator)).Click();
        }


        public void WaitUntilVisible(By locator)
        {
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Message = "Element with locator '" + locator + "' was not visible in 10 seconds";
            wait.Until(driver => driver.FindElement(locator).Displayed);
        }

        public void WaitUntilClickable(By locator)
        {
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Message = "Element with locator '" + locator + "' was not clickable in 10 seconds";
            wait.Until(driver => driver.FindElement(locator).Enabled);
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void FillTheField(By locator, string value)
        {
            driver.FindElement(locator).Clear();
            driver.FindElement(locator).SendKeys(value);
        }

        public void FillTheDropdown(string name, string value)
        {
            driver.FindElement(ContactPage.DropdownByName(name)).Click();
            driver.FindElement(ContactPage.DropdownByNameAndValue(name, value)).Click();
        }

        public string GetValueByName(string name)
        {
            string[] dropdown = { "bday", "bmonth", "aday", "amonth" };
            if (dropdown.Any(name.Contains))
            {
                return driver.FindElement(ContactPage.DropdownOptionText(name)).Text;
            }
            else
            {
                return driver.FindElement(By.Name(name)).GetAttribute("value");
            }

        }

        public int GetGroupCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.XPath("//tr[@name=\"entry\"]")).Count;
        }
      
    }
}
