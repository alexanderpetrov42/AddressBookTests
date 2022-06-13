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
            app.Navigation.OpenHomePage();
            List<ContactData> oldContacts = app.Contact.GetContactList();
            app.Contact.CreateContact(contact);
            app.Navigation.OpenHomePage();
            Assert.AreEqual(oldContacts.Count, app.Contact.GetContactCount() - 1);
            List<ContactData> newContacts = app.Contact.GetContactList();

            newContacts.Sort();;
            contact.Id = newContacts[newContacts.Count - 1].Id;
            oldContacts.Add(contact);
            oldContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

            app.Contact.AssertContactValues(contact);
        }

        [Test, TestCaseSource("GroupDataFromXmlFile")]
        public void AddGroup(GroupData group)
        {
            List<GroupData> oldGroups = app.Group.GetGroupList();
            app.Group.CreateGroup(group);
            app.Navigation.OpenGroupsPage();
            Assert.AreEqual(oldGroups.Count, app.Group.GetGroupCount() - 1);
            List<GroupData> newGroups = app.Group.GetGroupList();

            newGroups.Sort();
            group.Id = newGroups[newGroups.Count - 1].Id;
            oldGroups.Add(group);
            oldGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            app.Group.AssertGroupCreatedOrEdited(group);
        }

        [Test, TestCaseSource("GroupDataFromXmlFile")]
        public void EditGroup(GroupData group)
        {
            app.Navigation.OpenGroupsPage();

            if (!app.Group.IsGroupPresented())
            {
                AddGroup(group);
            }

            List<GroupData> oldGroups = app.Group.GetGroupList();
            oldGroups.Sort();

            GroupData edit_group = new GroupData(group.Name + "_edited") { Header = group.Header + "_edited", Footer = group.Footer + "_edited" };
            edit_group.Id = oldGroups[oldGroups.Count - 1].Id;
            string lastGroupValue = app.Group.EditLastCreatedGroup(edit_group);
            List<GroupData> newGroups = app.Group.GetGroupList();
            newGroups.Sort();

            Assert.AreEqual(edit_group, newGroups[newGroups.Count-1]);
            app.Group.AssertGroupCreatedOrEdited(edit_group);
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
