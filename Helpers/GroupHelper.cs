using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AddressBook
{
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager manager):base(manager)
        {
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
            FillTheField(GroupPage.GroupFooter, group.Footer); ;
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
            manager.Navigation.OpenHomePage();
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

    }
}
