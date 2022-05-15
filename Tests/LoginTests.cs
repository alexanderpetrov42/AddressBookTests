using NUnit.Framework;

namespace AddressBook
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidData()
        {
            app.Auth.Logout();
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);
            Assert.True(app.Auth.IsLoggedIn(account.User));
            Assert.True(app.Auth.IsLoggedIn());

        }

        [Test]
        public void LoginWithInvalidData()
        {
            app.Auth.Logout();
            AccountData account = new AccountData("invalid", "data");
            app.Auth.Login(account);
            Assert.False(app.Auth.IsLoggedIn(account.User));
            Assert.False(app.Auth.IsLoggedIn());
        }
    }
}
