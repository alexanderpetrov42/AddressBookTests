using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    public class GroupData : IComparable<GroupData>
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

        public int Id { get; set; }

        int IComparable<GroupData>.CompareTo(GroupData? other)
        {
            return Id.CompareTo(other.Id);
        }

        public override bool Equals(object group)
        {
            var toCompareWith = group as GroupData;
            if (toCompareWith == null)
                return false;
            return
                this.Id == toCompareWith.Id &&
                this.Name == toCompareWith.Name;
        }
    }
}
