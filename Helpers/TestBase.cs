using NUnit.Framework;

namespace AddressBook
{
    public class TestBase
    {
        private LoginPage loginPage;
        private ContactPage contactPage;
        private GroupPage groupPage;
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

        public ContactPage ContactPage
        {
            get
            {
                if (contactPage == null)
                {
                    contactPage = new ContactPage();
                }

                return contactPage;
            }
        }

        public GroupPage GroupPage
        {
            get
            {
                if (groupPage == null)
                {
                    groupPage = new GroupPage();
                }

                return groupPage;
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
