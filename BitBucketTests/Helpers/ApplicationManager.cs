using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace BitBucketTests
{
    public class ApplicationManager
    {
        protected internal IWebDriver driver;
        private string baseURL;

        private RepoHelper repo;
        private LoginHelper auth;
        private NavigationHelper navigation;

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            { 
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Navigation.OpenHomePage();
                app.Value = newInstance;
            }
            return app.Value;
        }

        private ApplicationManager()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.Manage().Window.Maximize();

            repo = new RepoHelper(this);
            auth = new LoginHelper(this);
            navigation = new NavigationHelper(this, Settings.BaseURL);
        }

        ~ApplicationManager()
        {
            driver.Quit();
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

        public RepoHelper Repo
        {
            get
            {
                return repo;
            }
        }


        public void Stop()
        {
            driver.Quit();
        }

    }
}
