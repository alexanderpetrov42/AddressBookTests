using NUnit.Framework;

namespace BitBucketTests.Helpers
{
    public class AuthBase : TestBase
    {
        [SetUp]
        public void SetupApplicationManagerAuthBase()
        {
            app = ApplicationManager.GetInstance();
            app.Navigation.OpenHomePage();
            AccountData account = new AccountData(Settings.Login, Settings.Password);
            app.Auth.Login(account);
        }        
    }
}
