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
    public class LoginHelper : HelperBase
    {

        protected internal void Login(AccountData account)
        {
            FillTheField(LoginPage.UserTextField, account.User);
            FillTheField(LoginPage.PasswordTextField, account.Password);
            Click(LoginPage.SubmitLoginButton);
        }

        public LoginHelper(ApplicationManager manager):base(manager)
        {
        }

    }
}
