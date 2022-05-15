using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AddressBook
{
    public class LoginPage
    {
        public By UserTextField { get { return By.Name("user"); } }

        public By PasswordTextField { get { return By.Name("pass"); } }

        public By SubmitLoginButton { get { return By.XPath("//input[@type=\"submit\"][@value=\"Login\"]"); } }

        public By LogoutButton { get { return By.XPath("//form[@name=\"logout\"]//a[text()=\"Logout\"]"); } }

        public By LoggedInUser(string user) => By.XPath($"//form[@name=\"logout\"]//b[text()=\"({user})\"]");
    }
}
