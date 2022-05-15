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

        public By SubmitLoginButton { get { return By.CssSelector("input[type=\"submit\"]"); } }
    }
}
