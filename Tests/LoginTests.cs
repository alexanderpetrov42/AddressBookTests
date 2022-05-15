using NUnit.Framework;

namespace AddressBook
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidData()
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
        public void LoginWithInvalidData()
        {
            if (app.Auth.IsLoggedIn())
            { 
                app.Auth.Logout();
            }
            AccountData account = new AccountData("invalid", "data");
            app.Auth.Login(account);
            Assert.False(app.Auth.IsLoggedIn(account.User));
            Assert.False(app.Auth.IsLoggedIn());
        }
    }
}
