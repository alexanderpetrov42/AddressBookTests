using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Helpers
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
