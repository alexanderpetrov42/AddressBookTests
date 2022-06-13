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

        protected internal void AssertGroupCreatedOrEdited(GroupData group)
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

        public string ClickEditOnLastCreatedGroup()
        {
            driver.FindElement(GroupPage.GroupsTab).Click();
            string lastGroupValue = SelectLastCreatedGroup();
            driver.FindElement(GroupPage.Edit).Click();
            return lastGroupValue;
        }

        protected internal string EditLastCreatedGroup(GroupData group)
        {
            string lastGroupValue = ClickEditOnLastCreatedGroup();
            FillTheField(GroupPage.GroupName, group.Name);
            FillTheField(GroupPage.GroupHeader, group.Header);
            FillTheField(GroupPage.GroupFooter, group.Footer);
            driver.FindElement(GroupPage.Update).Click();
            return lastGroupValue;
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

        public List<GroupData> GetGroupList()
        {
            List<GroupData> groups = new List<GroupData>();
            manager.Navigation.OpenGroupsPage();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));

            foreach (IWebElement element in elements)
            {
                groups.Add(new GroupData(element.Text)
                {
                    Id = Int32.Parse(element.FindElement(By.TagName("input")).GetAttribute("value"))
                }); ;
            }
            return groups;
        }
    }
}
