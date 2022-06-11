using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    public class GroupData
    {
        public GroupData(string name)
        {
            Name = name;
        }

        public GroupData()
        {
        }

        public string Name { get; set; }

        public string Header { get; set; }

        public string Footer { get; set; }
    }
}
