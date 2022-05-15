using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace AddressBook
{
    public class ApplicationManager
    {
        protected internal IWebDriver driver;
        private string baseURL;

        private NavigationHelper navigation;
        private LoginHelper auth;
        private GroupHelper group;
        private ContactHelper contact;


        public ApplicationManager()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            driver.Manage().Window.Maximize();

            baseURL = "http://localhost/addressbook/";

            group = new GroupHelper(this);
            contact = new ContactHelper(this);
            auth = new LoginHelper(this);
            navigation = new NavigationHelper(this, baseURL);


            Navigation.OpenHomePage();
            AccountData account = new AccountData("admin", "secret");
            Auth.Login(account);
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }
        public NavigationHelper Navigation
        {
            get
            {
                return navigation;
            }
        }

        public LoginHelper Auth
        {
            get
            {
                return auth;
            }
        }
        public GroupHelper Group
        {
            get
            {
                return group;
            }
        }
        public ContactHelper Contact
        {
            get
            {
                return contact;
            }
        }


        public void Stop()
        {
            driver.Quit();
        }

    }
}
