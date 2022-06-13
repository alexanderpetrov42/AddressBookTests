using NUnit.Framework;

namespace BitBucketTests
{
    public class TestBase
    {
        private LoginPage loginPage;
        private HomePage homePage;
        protected ApplicationManager app;


        #region Pages
        public LoginPage LoginPage
        {
            get
            {
                if (loginPage == null)
                {
                    loginPage = new LoginPage();
                }

                return loginPage;
            }
        }

        public HomePage ContactPage
        {
            get
            {
                if (homePage == null)
                {
                    homePage = new HomePage();
                }

                return homePage;
            }
        }

        #endregion

        [SetUp]
        public void SetUpApplicationManager()
        {
            app = ApplicationManager.GetInstance();
            app.Navigation.OpenHomePage();
        }
    }
}
