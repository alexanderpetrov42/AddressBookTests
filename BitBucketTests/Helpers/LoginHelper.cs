using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

namespace BitBucketTests
{
    public class LoginHelper : HelperBase
    {

        protected internal void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                return;
            }
            FillTheField(LoginPage.UserTextField, account.User);
            Click(LoginPage.SubmitUserButton);
            FillTheField(LoginPage.PasswordTextField, account.Password);
            Click(LoginPage.SubmitLoginButton);
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(LoginPage.ProfileButton);
        }

        public bool IsLoggedIn(string username)
        {
            try
            {
                Click(LoginPage.ProfileButton);
                return IsElementPresent(LoginPage.LoggedInUser(username));
            }
            catch {
                return false;
            }
        }

        public void Logout()
        {
            WaitUntilClickable(LoginPage.ProfileButton);
            Click(LoginPage.ProfileButton);
            Click(LoginPage.LogoutButton);
        }

        public LoginHelper(ApplicationManager manager):base(manager)
        {
        }

    }
}
