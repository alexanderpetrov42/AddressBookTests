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
    public class TestBase
    {
        protected WebDriverWait wait;
        protected internal IWebDriver driver;
        private LoginPage loginPage;
        private ContactPage contactPage;
        private GroupPage groupPage;
        public IDictionary<string, object> vars { get; private set; }
        private IJavaScriptExecutor js;


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

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<string, object>();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            OpenHomePage();
            AccountData account = new AccountData("admin", "secret");
            Login(account);
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
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Message = "Element with locator '" + locator + "' was not visible in 20 seconds";
            wait.Until(driver => driver.FindElement(locator).Displayed);
        }

        public void WaitUntilClickable(By locator)
        {
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Message = "Element with locator '" + locator + "' was not clickable in 20 seconds";
            wait.Until(driver => driver.FindElement(locator).Enabled);
        }

        private void Login(AccountData account)
        {
            FillTheField(LoginPage.UserTextField, account.User);
            FillTheField(LoginPage.PasswordTextField, account.Password);
            Click(LoginPage.SubmitLoginButton);
        }
        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch(NoSuchElementException)
            {
                return false;
            }
        }
        private void OpenHomePage()
        {
            driver.Navigate().GoToUrl("http://localhost/addressbook/");
            driver.Manage().Window.Size = new System.Drawing.Size(1024, 728);
        }

        [TearDown]
        protected void TearDown()
        {
            driver.Quit();
        }

        public void FillTheField(By locator, string value)
        {
            driver.FindElement(locator).Clear();
            driver.FindElement(locator).SendKeys(value);
        }

        public void FillTheDropdown(string name, string value)
        {
            driver.FindElement(By.CssSelector($"select[name = \"{name}\"]")).Click();
            driver.FindElement(By.XPath($"//select[@name = \"{name}\"]/option[text()=\"{value}\"]")).Click();
        }

        public string GetValueByName(string name)
        {
            string[] dropdown = { "bday", "bmonth", "aday", "amonth" };
            if (dropdown.Any(name.Contains))
            {
                return driver.FindElement(By.XPath($"//select[@name = \"{name}\"]/option[text()]")).Text;
            }
            else
            {
                return driver.FindElement(By.Name(name)).GetAttribute("value");
            }

        }

        protected internal void CreateContact(ContactData contact)
        {
            driver.FindElement(ContactPage.AddNewTab).Click();

            FillTheField(ContactPage.FirstName, contact.FirstName);
            FillTheField(ContactPage.MiddleName, contact.MiddleName);
            FillTheField(ContactPage.LastName, contact.LastName);
            FillTheField(ContactPage.Nickname, contact.Nickname);
            FillTheField(ContactPage.Title, contact.Title);
            FillTheField(ContactPage.Company, contact.Company);
            FillTheField(ContactPage.Address, contact.Address);
            FillTheField(ContactPage.Home, contact.HomeTelephone);
            FillTheField(ContactPage.Mobile, contact.MobileTelephone);
            FillTheField(ContactPage.Work, contact.WorkTelephone);
            FillTheField(ContactPage.Fax, contact.FaxTelephone);
            FillTheField(ContactPage.Email, contact.Email);
            FillTheField(ContactPage.Email2, contact.Email2);
            FillTheField(ContactPage.Email3, contact.Email3);
            FillTheField(ContactPage.Homepage, contact.Homepage);
            FillTheDropdown("bday", contact.BirthdayDay);
            FillTheDropdown("bmonth", contact.BirthdayMonth);
            FillTheField(ContactPage.Byear, contact.BirthdayYear);
            FillTheDropdown("aday", contact.AnniversaryDay);
            FillTheDropdown("amonth", contact.AnniversaryMonth);
            FillTheField(ContactPage.Ayear, contact.AnniversaryYear);
            FillTheField(ContactPage.Address2, contact.SecondaryAddress);
            FillTheField(ContactPage.Phone2, contact.SecondaryHome);
            FillTheField(ContactPage.Notes, contact.SecondaryNotes);

            driver.FindElement(ContactPage.Submit).Click();
        }

        protected internal void AssertContactValues(ContactData contact)
        {
            OpenHomePage();
            ClickEditOnLastCreatedContact();
            Assert.AreEqual(contact.FirstName, GetValueByName("firstname"));
            Assert.AreEqual(contact.MiddleName, GetValueByName("middlename"));
            Assert.AreEqual(contact.LastName, GetValueByName("lastname"));
            Assert.AreEqual(contact.Nickname, GetValueByName("nickname"));
            Assert.AreEqual(contact.Title, GetValueByName("title"));
            Assert.AreEqual(contact.Company, GetValueByName("company"));
            Assert.AreEqual(contact.Address, GetValueByName("address"));
            Assert.AreEqual(contact.HomeTelephone, GetValueByName("home"));
            Assert.AreEqual(contact.MobileTelephone, GetValueByName("mobile"));
            Assert.AreEqual(contact.WorkTelephone, GetValueByName("work"));
            Assert.AreEqual(contact.FaxTelephone, GetValueByName("fax"));
            Assert.AreEqual(contact.Email, GetValueByName("email"));
            Assert.AreEqual(contact.Email2, GetValueByName("email2"));
            Assert.AreEqual(contact.Email3, GetValueByName("email3"));
            Assert.AreEqual(contact.Homepage, GetValueByName("homepage"));
            Assert.AreEqual(contact.BirthdayDay, GetValueByName("bday"));
            Assert.AreEqual(contact.BirthdayMonth, GetValueByName("bmonth"));
            Assert.AreEqual(contact.BirthdayYear, GetValueByName("byear"));
            Assert.AreEqual(contact.AnniversaryDay, GetValueByName("aday"));
            Assert.AreEqual(contact.AnniversaryMonth, GetValueByName("amonth"));
            Assert.AreEqual(contact.AnniversaryYear, GetValueByName("ayear"));
            Assert.AreEqual(contact.SecondaryAddress, GetValueByName("address2"));
            Assert.AreEqual(contact.SecondaryHome, GetValueByName("phone2"));
            Assert.AreEqual(contact.SecondaryNotes, GetValueByName("notes"));
        }
        public bool IsGroupPresented()
        {
            return IsElementPresent(GroupPage.GroupElem);
        }

        protected internal void CreateGroup(GroupData group)
        {
            driver.FindElement(GroupPage.GroupsTab).Click();
            driver.FindElement(GroupPage.New).Click();
            FillTheField(GroupPage.GroupName, group.Name);
            FillTheField(GroupPage.GroupHeader, group.Header);
            FillTheField(GroupPage.GroupFooter, group.Footer);;
            driver.FindElement(GroupPage.Submit).Click();
        }

        protected internal void AssertGroupCreated(GroupData group)
        {
            ClickEditOnLastCreatedGroup();
            Assert.AreEqual(group.Name, GetValueByName("group_name"));
            Assert.AreEqual(group.Header, GetValueByName("group_header"));
            Assert.AreEqual(group.Footer, GetValueByName("group_footer"));
        }

        public GroupData GetCreatedGroupData()
        {
            string groupName = driver.FindElement(By.Name("group_name")).GetAttribute("value");
            string header = driver.FindElement(By.Name("group_header")).Text;
            string footer = driver.FindElement(By.Name("group_footer")).Text;
            return new GroupData(groupName) { Header = header, Footer = footer };
        }

        public void ClickEditOnLastCreatedGroup()
        {
            driver.FindElement(GroupPage.GroupsTab).Click();
            SelectLastCreatedGroup();
            driver.FindElement(GroupPage.Edit).Click();
        }

        protected internal void EditLastCreatedGroup(GroupData group)
        {
            ClickEditOnLastCreatedGroup();
            FillTheField(GroupPage.GroupName, group.Name);
            FillTheField(GroupPage.GroupHeader, group.Header);
            FillTheField(GroupPage.GroupFooter, group.Footer);
            driver.FindElement(GroupPage.Update).Click();
        }

        protected internal string DeleteLastCreatedGroup()
        {
            driver.FindElement(GroupPage.GroupsTab).Click();
            string lgv = SelectLastCreatedGroup();
            driver.FindElement(GroupPage.Delete).Click();
            return lgv;
        }

        protected internal void AssertLastCreatedGroupDeleted(string last_group_value)
        {
            OpenHomePage();
            driver.FindElement(GroupPage.GroupsTab).Click();
            Assert.IsTrue(driver.FindElements(GroupPage.LastGroup(last_group_value)).Count == 0);
        }

        protected internal string SelectLastCreatedGroup()
        {
            List<int> values = new List<int>();
            var allGroupsValues = driver.FindElements(GroupPage.AllGroupsValues);
            foreach (IWebElement i in allGroupsValues)
            {
                values.Add(Int32.Parse(i.GetAttribute("value")));
            }
            string last_group_value = values.Max().ToString();
            driver.FindElement(GroupPage.LastGroup(last_group_value)).Click();
            return last_group_value;
        }
        public bool IsContactPresented()
        {
            return IsElementPresent(ContactPage.ContactEntry);
        }

        protected internal string ClickEditOnLastCreatedContact()
        {
            string lcc_id = GetLastCreatedContactId();
            driver.FindElement(ContactPage.EditLastCreatedContact(lcc_id)).Click();
            return lcc_id;
        }

        protected internal string GetLastCreatedContactId()
        {
            OpenHomePage();
            List<int> ids = new List<int>();
            var allContactsInputs = driver.FindElements(ContactPage.AllContactsInputs);
            foreach (IWebElement i in allContactsInputs)
            {
                ids.Add(Int32.Parse(i.GetAttribute("id")));
            }
            return ids.Max().ToString();
        } 

        protected internal void EditLastCreatedContact(ContactData contact)
        {
            ClickEditOnLastCreatedContact();
            FillTheField(ContactPage.FirstName, contact.FirstName);
            FillTheField(ContactPage.MiddleName, contact.MiddleName);
            FillTheField(ContactPage.LastName, contact.LastName);
            FillTheField(ContactPage.Nickname, contact.Nickname);
            FillTheField(ContactPage.Title, contact.Title);
            FillTheField(ContactPage.Company, contact.Company);
            FillTheField(ContactPage.Address, contact.Address);
            FillTheField(ContactPage.Home, contact.HomeTelephone);
            FillTheField(ContactPage.Mobile, contact.MobileTelephone);
            FillTheField(ContactPage.Work, contact.WorkTelephone);
            FillTheField(ContactPage.Fax, contact.FaxTelephone);
            FillTheField(ContactPage.Email, contact.Email);
            FillTheField(ContactPage.Email2, contact.Email2);
            FillTheField(ContactPage.Email3, contact.Email3);
            FillTheField(ContactPage.Homepage, contact.Homepage);
            FillTheDropdown("bday", contact.BirthdayDay);
            FillTheDropdown("bmonth", contact.BirthdayMonth);
            FillTheField(ContactPage.Byear, contact.BirthdayYear);
            FillTheDropdown("aday", contact.AnniversaryDay);
            FillTheDropdown("amonth", contact.AnniversaryMonth);
            FillTheField(ContactPage.Ayear, contact.AnniversaryYear);
            FillTheField(ContactPage.Address2, contact.SecondaryAddress);
            FillTheField(ContactPage.Phone2, contact.SecondaryHome);
            FillTheField(ContactPage.Notes, contact.SecondaryNotes);

            driver.FindElement(By.Name("update")).Click();
        }
        protected internal string DeleteLastCreatedContact()
        {
            OpenHomePage();
            string lcc_id = GetLastCreatedContactId();
            driver.FindElement(By.Id(lcc_id)).Click();
            driver.FindElement(ContactPage.DeleteContact).Click();
            driver.SwitchTo().Alert().Accept();
            return lcc_id;
        }

        protected internal void AssertLastContactDeleted(string contact)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(Condition(By.Id("search_count")));
            Assert.True(driver.FindElements(By.Id(contact)).Count == 0);
        }

    }
}
