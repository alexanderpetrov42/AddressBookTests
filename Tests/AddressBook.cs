using AddressBook.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace AddressBook
{

    [TestFixture]
    public class AddressBookTests : AuthBase
    {
        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            return (List<GroupData>)new XmlSerializer(typeof(List<GroupData>))
                .Deserialize(new StreamReader(@"groups.xml"));
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)new XmlSerializer(typeof(List<ContactData>))
                .Deserialize(new StreamReader(@"contacts.xml"));
        }

        [Test, TestCaseSource("ContactDataFromXmlFile")]
        public void AddContact(ContactData contact)
        {
            app.Contact.CreateContact(contact);
            app.Contact.AssertContactValues(contact);
        }

        [Test, TestCaseSource("GroupDataFromXmlFile")]
        public void AddGroup(GroupData group)
        {
            app.Group.CreateGroup(group);
            app.Group.AssertGroupCreated(group);
        }

        [Test, TestCaseSource("GroupDataFromXmlFile")]
        public void EditGroup(GroupData group)
        {
            if (!app.Group.IsGroupPresented())
            {
                AddGroup(group);
            }
            app.Group.EditLastCreatedGroup(group);
        }

        [Test, TestCaseSource("GroupDataFromXmlFile")]
        public void DeleteGroup(GroupData group)
        {
            if (!app.Group.IsGroupPresented())
            {
                AddGroup(group);
            }

            app.Group.AssertLastCreatedGroupDeleted(app.Group.DeleteLastCreatedGroup());
        }

        [Test, TestCaseSource("ContactDataFromXmlFile")]
        public void EditContact(ContactData contact)
        {
            if (!app.Contact.IsContactPresented())
            {
                AddContact(contact);
            }

            app.Contact.EditLastCreatedContact(contact);
            app.Contact.AssertContactValues(contact);
        }

        [Test, TestCaseSource("ContactDataFromXmlFile")]
        public void DeleteContact(ContactData contact)
        {
            if (!app.Contact.IsContactPresented())
            {
                AddContact(contact);
            }

            app.Contact.AssertLastContactDeleted(app.Contact.DeleteLastCreatedContact());

        }
    }
}
