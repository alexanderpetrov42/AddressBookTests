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
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account.User))
                {
                    return;
                }
                Logout();
            }
            FillTheField(LoginPage.UserTextField, account.User);
            FillTheField(LoginPage.PasswordTextField, account.Password);
            Click(LoginPage.SubmitLoginButton);
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(LoginPage.LogoutButton);
        }

        public bool IsLoggedIn(string username)
        {
            return IsElementPresent(LoginPage.LoggedInUser(username));
        }

        public void Logout()
        {
            Click(LoginPage.LogoutButton);
        }

        public LoginHelper(ApplicationManager manager):base(manager)
        {
        }

    }
}
