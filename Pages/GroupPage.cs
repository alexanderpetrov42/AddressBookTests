using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AddressBook
{
    public class GroupPage
    {
        public By GroupsTab { get { return By.LinkText("groups"); } }
        public By New { get { return By.Name("new"); } }
        public By GroupName { get { return By.Name("group_name"); } }
        public By GroupHeader { get { return By.Name("group_header"); } }
        public By GroupFooter { get { return By.Name("group_footer"); } }
        public By Submit { get { return By.Name("submit"); } }
        public By AllGroupsValues { get { return By.XPath("//span[@class=\"group\"]/input"); } }
        public By Edit { get { return By.Name("edit"); } }
        public By Delete { get { return By.Name("delete"); } }
        public By Update { get { return By.Name("update"); } }
        public By GroupElem { get { return By.XPath("//span[@class=\"group\"]"); } }
        public By LastGroup(string last_group_value) => By.XPath($"//span[@class=\"group\"]/input[@value=\"{last_group_value}\"]");
    }
}
