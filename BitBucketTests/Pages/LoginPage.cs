using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BitBucketTests
{
    public class LoginPage
    {
        public By UserTextField { get { return By.Name("username"); } }
        public By SubmitUserButton { get { return By.XPath("//button[@type=\"submit\"]"); } }
        public By PasswordTextField { get { return By.Name("password"); } }
        public By SubmitLoginButton { get { return By.Id("login-submit"); } }
        public By ProfileButton { get { return By.XPath("//button[@data-testid=\"profile-button\"]"); } }
        public By LogoutButton { get { return By.XPath("//span[text()=\"Log out\"]"); } }




        public By LoggedInUser(string user) => By.XPath($"//form[@name=\"logout\"]//b[text()=\"({user})\"]");
    }
}
