using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    public class ContactData : IComparable<ContactData>
    {
        public ContactData(string firstName)
        {
            FirstName = firstName;
        }

        public ContactData()
        {
        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string HomeTelephone { get; set; }
        public string MobileTelephone { get; set; }
        public string WorkTelephone { get; set; }
        public string FaxTelephone { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Homepage { get; set; }
        public string BirthdayDay { get; set; }
        public string BirthdayMonth { get; set; }
        public string BirthdayYear { get; set; }
        public string AnniversaryDay { get; set; }
        public string AnniversaryMonth { get; set; }
        public string AnniversaryYear { get; set; }
        public string SecondaryAddress { get; set; }
        public string SecondaryHome { get; set; }
        public string SecondaryNotes { get; set; }
        public int Id { get; set; }

        int IComparable<ContactData>.CompareTo(ContactData? other)
        {
            return Id.CompareTo(other.Id);
        }

        public override bool Equals(object contact)
        {
            var toCompareWith = contact as ContactData;
            if (toCompareWith == null)
                return false;
            return
                this.Id == toCompareWith.Id &&
                this.FirstName == toCompareWith.FirstName &&
                this.LastName == toCompareWith.LastName &&
                this.Address == toCompareWith.Address;
        }
    }

}
