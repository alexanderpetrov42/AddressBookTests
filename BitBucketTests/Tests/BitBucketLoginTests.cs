using NUnit.Framework;

namespace BitBucketTests
{
    [TestFixture]
    public class BitBucketLoginTests : TestBase
    {
        [Test]
        public void LoginWithValidDataBitbucket()
        {
            if (app.Auth.IsLoggedIn())
            {
                app.Auth.Logout();
            }

            AccountData account = new AccountData(Settings.Login, Settings.Password);
            app.Auth.Login(account);
            Assert.True(app.Auth.IsLoggedIn(account.User));
            Assert.True(app.Auth.IsLoggedIn());

        }

        [Test]
        public void LoginWithInvalidDataBitbucket()
        {
            if (app.Auth.IsLoggedIn())
            {
                app.Auth.Logout();
            }
            AccountData account = new AccountData("invalid@data.data", "invaliddata");
            app.Auth.Login(account);
            Assert.False(app.Auth.IsLoggedIn(account.User));
            Assert.False(app.Auth.IsLoggedIn());
        }

        [Test]
        public void LogoutBitbucket()
        {

            if (app.Auth.IsLoggedIn())
            {
                app.Auth.Logout();
            }

            AccountData account = new AccountData(Settings.Login, Settings.Password);
            app.Auth.Login(account);
            app.Auth.Logout();

            Assert.False(app.Auth.IsLoggedIn());
            Assert.False(app.Auth.IsLoggedIn(account.User));
        }
    }
}
