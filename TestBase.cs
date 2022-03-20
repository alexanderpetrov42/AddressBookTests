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
        protected internal IWebDriver driver;
        public IDictionary<string, object> vars { get; private set; }
        private IJavaScriptExecutor js;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<string, object>();
            OpenHomePage();
            AccountData account = new AccountData("admin", "secret");
            Login(account);
        }

        private void Login(AccountData account)
        {
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(account.User);
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
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
            driver.FindElement(By.LinkText("add new")).Click();

            FillTheField(By.Name("firstname"), contact.FirstName);
            FillTheField(By.Name("middlename"), contact.MiddleName);
            FillTheField(By.Name("lastname"), contact.LastName);
            FillTheField(By.Name("nickname"), contact.Nickname);
            FillTheField(By.Name("title"), contact.Title);
            FillTheField(By.Name("company"), contact.Company);
            FillTheField(By.Name("address"), contact.Address);
            FillTheField(By.Name("home"), contact.HomeTelephone);
            FillTheField(By.Name("mobile"), contact.MobileTelephone);
            FillTheField(By.Name("work"), contact.WorkTelephone);
            FillTheField(By.Name("fax"), contact.FaxTelephone);
            FillTheField(By.Name("email"), contact.Email);
            FillTheField(By.Name("email2"), contact.Email2);
            FillTheField(By.Name("email3"), contact.Email3);
            FillTheField(By.Name("homepage"), contact.Homepage);
            FillTheDropdown("bday", contact.BirthdayDay);
            FillTheDropdown("bmonth", contact.BirthdayMonth);
            FillTheField(By.Name("byear"), contact.BirthdayYear);
            FillTheDropdown("aday", contact.AnniversaryDay);
            FillTheDropdown("amonth", contact.AnniversaryMonth);
            FillTheField(By.Name("ayear"), contact.AnniversaryYear);
            FillTheField(By.Name("address2"), contact.SecondaryAddress);
            FillTheField(By.Name("phone2"), contact.SecondaryHome);
            FillTheField(By.Name("notes"), contact.SecondaryNotes);

            driver.FindElement(By.Name("submit")).Click();
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
            return IsElementPresent(By.XPath("//span[@class=\"group\"]"));
        }

        protected internal void CreateGroup(GroupData group)
        {
            driver.FindElement(By.LinkText("groups")).Click();
            driver.FindElement(By.Name("new")).Click();
            FillTheField(By.Name("group_name"), group.Name);
            FillTheField(By.Name("group_header"), group.Header);
            FillTheField(By.Name("group_footer"), group.Footer);;
            driver.FindElement(By.Name("submit")).Click();
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
            driver.FindElement(By.LinkText("groups")).Click();
            SelectLastCreatedGroup();
            driver.FindElement(By.Name("edit")).Click();
        }

        protected internal void EditLastCreatedGroup(GroupData group)
        {
            ClickEditOnLastCreatedGroup();
            FillTheField(By.Name("group_name"), group.Name);
            FillTheField(By.Name("group_header"), group.Header);
            FillTheField(By.Name("group_footer"), group.Footer);
            driver.FindElement(By.Name("update")).Click();
        }

        protected internal string DeleteLastCreatedGroup()
        {
            driver.FindElement(By.LinkText("groups")).Click();
            string lgv = SelectLastCreatedGroup();
            driver.FindElement(By.Name("delete")).Click();
            return lgv;
        }

        protected internal void AssertLastCreatedGroupDeleted(string last_group_value)
        {
            OpenHomePage();
            driver.FindElement(By.LinkText("groups")).Click();
            Assert.IsTrue(driver.FindElements(By.XPath($"//span[@class=\"group\"]/input[@value=\"{last_group_value}\"]")).Count == 0);
        }

        protected internal string SelectLastCreatedGroup()
        {
            List<int> values = new List<int>();
            var allGroupsValues = driver.FindElements(By.XPath("//span[@class=\"group\"]/input"));
            foreach (IWebElement i in allGroupsValues)
            {
                values.Add(Int32.Parse(i.GetAttribute("value")));
            }
            string last_group_value = values.Max().ToString();
            driver.FindElement(By.XPath($"//span[@class=\"group\"]/input[@value=\"{last_group_value}\"]")).Click();
            return last_group_value;
        }
        public bool IsContactPresented()
        {
            return IsElementPresent(By.XPath("//tr[@name=\"entry\"]"));
        }

        protected internal string ClickEditOnLastCreatedContact()
        {
            string lcc_id = GetLastCreatedContactId();
            driver.FindElement(By.XPath($"//tr[@name=\"entry\"]/td/a[@href=\"edit.php?id={lcc_id}\"]")).Click();
            return lcc_id;
        }

        protected internal string GetLastCreatedContactId()
        {
            OpenHomePage();
            List<int> ids = new List<int>();
            var allContactsInputs = driver.FindElements(By.XPath("//tr[@name=\"entry\"]/td/input"));
            foreach (IWebElement i in allContactsInputs)
            {
                ids.Add(Int32.Parse(i.GetAttribute("id")));
            }
            return ids.Max().ToString();
        } 

        protected internal void EditLastCreatedContact(ContactData contact)
        {
            ClickEditOnLastCreatedContact();
            FillTheField(By.Name("firstname"), contact.FirstName);
            FillTheField(By.Name("middlename"), contact.MiddleName);
            FillTheField(By.Name("lastname"), contact.LastName);
            FillTheField(By.Name("nickname"), contact.Nickname);
            FillTheField(By.Name("title"), contact.Title);
            FillTheField(By.Name("company"), contact.Company);
            FillTheField(By.Name("address"), contact.Address);
            FillTheField(By.Name("home"), contact.HomeTelephone);
            FillTheField(By.Name("mobile"), contact.MobileTelephone);
            FillTheField(By.Name("work"), contact.WorkTelephone);
            FillTheField(By.Name("fax"), contact.FaxTelephone);
            FillTheField(By.Name("email"), contact.Email);
            FillTheField(By.Name("email2"), contact.Email2);
            FillTheField(By.Name("email3"), contact.Email3);
            FillTheField(By.Name("homepage"), contact.Homepage);
            FillTheDropdown("bday", contact.BirthdayDay);
            FillTheDropdown("bmonth", contact.BirthdayMonth);
            FillTheField(By.Name("byear"), contact.BirthdayYear);
            FillTheDropdown("aday", contact.AnniversaryDay);
            FillTheDropdown("amonth", contact.AnniversaryMonth);
            FillTheField(By.Name("ayear"), contact.AnniversaryYear);
            FillTheField(By.Name("address2"), contact.SecondaryAddress);
            FillTheField(By.Name("phone2"), contact.SecondaryHome);
            FillTheField(By.Name("notes"), contact.SecondaryNotes);

            driver.FindElement(By.Name("update")).Click();
        }
        protected internal string DeleteLastCreatedContact()
        {
            OpenHomePage();
            string lcc_id = GetLastCreatedContactId();
            driver.FindElement(By.Id(lcc_id)).Click();
            driver.FindElement(By.CssSelector("input[value=Delete]")).Click();
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(10000);
            return lcc_id;
        }

        protected internal void AssertLastContactDeleted(string contact)
        {
            Assert.True(driver.FindElements(By.Id(contact)).Count == 0);
        }

    }
}
