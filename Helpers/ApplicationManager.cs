using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace AddressBook
{
    public class ApplicationManager
    {
        protected internal IWebDriver driver;
        private string baseURL;

        public ApplicationManager()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            driver.Manage().Window.Maximize();

            baseURL = "http://localhost/addressbook/";

            Navigation = new NavigationHelper(this, this.baseURL);
            Auth = new LoginHelper(this);
            Group = new GroupHelper(this);
            Contact = new ContactHelper(this);

            Navigation.OpenHomePage();
            AccountData account = new AccountData("admin", "secret");
            Auth.Login(account);
        }

        public NavigationHelper Navigation { get; }
        public LoginHelper Auth { get; }
        public GroupHelper Group { get; }
        public ContactHelper Contact { get; }

        public void Stop()
        {
            driver.Quit();
        }

    }
}
