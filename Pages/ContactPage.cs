using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AddressBook
{
    public class ContactPage
    {
        public By AddNewTab { get { return By.LinkText("add new"); } }
        public By FirstName { get { return By.Name("firstname"); } }
        public By MiddleName { get { return By.Name("middlename"); } }
        public By LastName { get { return By.Name("lastname"); } }
        public By Nickname { get { return By.Name("nickname"); } }
        public By Title { get { return By.Name("title"); } }
        public By Company { get { return By.Name("company"); } }
        public By Address { get { return By.Name("address"); } }
        public By Home { get { return By.Name("home"); } }
        public By Mobile { get { return By.Name("mobile"); } }
        public By Work { get { return By.Name("work"); } }
        public By Fax { get { return By.Name("fax"); } }
        public By Email { get { return By.Name("email"); } }
        public By Email2 { get { return By.Name("email2"); } }
        public By Email3 { get { return By.Name("email3"); } }
        public By Homepage { get { return By.Name("homepage"); } }
        public By Bday { get { return By.Name("bday"); } }
        public By Bmonth { get { return By.Name("bmonth"); } }
        public By Byear { get { return By.Name("byear"); } }
        public By Aday { get { return By.Name("aday"); } }
        public By Amonth { get { return By.Name("amonth"); } }
        public By Ayear { get { return By.Name("ayear"); } }
        public By Address2 { get { return By.Name("address2"); } }
        public By Phone2 { get { return By.Name("phone2"); } }
        public By Notes { get { return By.Name("notes"); } }
        public By Submit { get { return By.Name("submit"); } }
        public By AllContactsInputs { get { return By.XPath("//tr[@name=\"entry\"]/td/input"); } }
        public By DeleteContact { get { return By.CssSelector("input[value=Delete]"); } }
        public By ContactEntry { get { return By.XPath("//tr[@name=\"entry\"]"); } }

        public By DropdownByName(string name) => By.CssSelector($"select[name = \"{name}\"]");

        public By DropdownByNameAndValue(string name, string value) => By.XPath($"//select[@name = \"{name}\"]/option[text()=\"{value}\"]");

        public By DropdownOptionText(string name) => By.XPath($"//select[@name = \"{name}\"]/option[text()]");

        public By EditLastCreatedContact(string lcc_id) => By.XPath($"//tr[@name=\"entry\"]/td/a[@href=\"edit.php?id={lcc_id}\"]");
    }
}
