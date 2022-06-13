using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace AddressBook
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base( manager)
        {
        }

        protected internal string ClickEditOnLastCreatedContact()
        {
            string lcc_id = GetLastCreatedContactId();
            driver.FindElement(ContactPage.EditLastCreatedContact(lcc_id)).Click();
            return lcc_id;
        }

        protected internal string GetLastCreatedContactId()
        {
            manager.Navigation.OpenHomePage();
            List<int> ids = new List<int>();
            var allContactsInputs = driver.FindElements(ContactPage.AllContactsInputs);
            foreach (IWebElement i in allContactsInputs)
            {
                ids.Add(Int32.Parse(i.GetAttribute("id")));
            }
            return ids.Max().ToString();
        }

        protected internal string DeleteLastCreatedContact()
        {
            manager.Navigation.OpenHomePage();
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

        protected internal bool IsContactPresented()
        {
            return IsElementPresent(ContactPage.ContactEntry);
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
            manager.Navigation.OpenHomePage();
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

        public List<ContactData> GetContactList()
        {
            manager.Navigation.OpenHomePage();

            List<ContactData> contacts = new List<ContactData>();
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//tr[@name=\"entry\"]"));

            foreach (IWebElement element in elements)
            {
                contacts.Add(new ContactData()
                {
                    Id = Int32.Parse(element.FindElement(By.TagName("input")).GetAttribute("value")),
                    FirstName = element.FindElements(By.TagName("td"))[2].Text,
                    LastName = element.FindElements(By.TagName("td"))[1].Text,
                    Address = element.FindElements(By.TagName("td"))[3].Text,
                });
            }
            return contacts;
        }

        public DateTime GenerateRandomDay()
        {
            Random rnd = new Random();
            DateTime start = new DateTime(1920, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }
    }
}
