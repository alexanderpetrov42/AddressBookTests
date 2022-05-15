
using AddressBook.Helpers;
using NUnit.Framework;

namespace AddressBook
{

    [TestFixture]
    public class AddressBookTests : AuthBase
    {
        [Test]
        public void AddContact()
        {
            ContactData contact = new ContactData { FirstName = "FirstName", MiddleName = "MiddleName", LastName = "LastName", Nickname = "Nickname", Company = "Company", Title = "Title", Address = "Address", HomeTelephone = "HomeTelephone", MobileTelephone = "MobileTelephone", WorkTelephone = "WorkTelephone", FaxTelephone = "FaxTelephone", Email = "Email", Email2 = "Email2", Email3 = "Email3", Homepage = "Homepage", BirthdayDay = "21", BirthdayMonth = "January", BirthdayYear = "1989", AnniversaryDay = "1", AnniversaryMonth = "January", AnniversaryYear = "2029", SecondaryAddress = "SecondaryAddress", SecondaryHome = "SecondaryHome", SecondaryNotes = "SecondaryNotes" };
            app.Contact.CreateContact(contact);
            app.Contact.AssertContactValues(contact);
        }

        [Test]
        public void AddGroup()
        {
            GroupData group = new GroupData("New Group") { Header = "sds", Footer = "dsfsd" };
            app.Group.CreateGroup(group);
            app.Group.AssertGroupCreated(group);
        }

        [Test]
        public void EditGroup()
        {
            if (!app.Group.IsGroupPresented())
            {
                AddGroup();
            }

            GroupData group = new GroupData("New Group_edited") { Header = "sds_edited", Footer = "dsfsd_edited" };
            app.Group.EditLastCreatedGroup(group);
        }

        [Test]
        public void DeleteGroup()
        {
            if (!app.Group.IsGroupPresented())
            {
                AddGroup();
            }

            app.Group.AssertLastCreatedGroupDeleted(app.Group.DeleteLastCreatedGroup());
        }

        [Test]
        public void EditContact()
        {
            if (!app.Contact.IsContactPresented())
            {
                AddContact();
            }

            ContactData contact = new ContactData { FirstName = "FirstName_edited", MiddleName = "MiddleName_edited", LastName = "LastName_edited", Nickname = "Nickname_edited", Company = "Company_edited", Title = "Title_edited", Address = "Address_edited", HomeTelephone = "HomeTelephone_edited", MobileTelephone = "MobileTelephone_edited", WorkTelephone = "WorkTelephone_edited", FaxTelephone = "FaxTelephone_edited", Email = "Email_edited", Email2 = "Email2_edited", Email3 = "Email3_edited", Homepage = "Homepage_edited", BirthdayDay = "22", BirthdayMonth = "July", BirthdayYear = "1990", AnniversaryDay = "1", AnniversaryMonth = "January", AnniversaryYear = "2039", SecondaryAddress = "SecondaryAddress_edited", SecondaryHome = "SecondaryHome_edited", SecondaryNotes = "SecondaryNotes_edited" };
            app.Contact.EditLastCreatedContact(contact);
            app.Contact.AssertContactValues(contact);
        }

        [Test]
        public void DeleteContact()
        {
            if (!app.Contact.IsContactPresented())
            {
                AddContact();
            }

            app.Contact.AssertLastContactDeleted(app.Contact.DeleteLastCreatedContact());

        }
    }
}
