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

namespace BitBucketTests
{
    public class HelperBase
    {
        public ApplicationManager manager;
        protected IWebDriver driver;
        protected WebDriverWait wait;
        private RepoHelper home;
        private LoginHelper auth;
        private NavigationHelper navigation;
        private LoginPage loginPage;
        private HomePage homePage;

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

        public HomePage HomePage
        {
            get
            {
                if (homePage == null)
                {
                    homePage = new HomePage();
                }

                return homePage;
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
    }
}
