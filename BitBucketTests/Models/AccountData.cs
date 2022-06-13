using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBucketTests
{
    public class AccountData
    {
        public AccountData(string user, string password)
        {
            User = user;
            Password = password;
        }

        public string User { get; set; }
        public string Password { get; set; }    
    }
}
