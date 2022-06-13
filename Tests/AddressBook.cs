using AddressBook.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            app.Navigation.OpenGroupsPage();

            if (!app.Group.IsGroupPresented())
            {
                AddGroup(group);
            }

            List<GroupData> oldGroups = app.Group.GetGroupList();
            oldGroups.Sort();
            string lastGroupValue = app.Group.DeleteLastCreatedGroup();

            app.Group.AssertLastCreatedGroupDeleted(lastGroupValue);
            int lgvIndex = oldGroups.FindIndex(p => p.Id == Convert.ToInt32(lastGroupValue));
            List<GroupData> newGroups = app.Group.GetGroupList();
            newGroups.Sort();
            oldGroups.RemoveAt(lgvIndex);
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test, TestCaseSource("ContactDataFromXmlFile")]
        public void EditContact(ContactData contact)
        {
            app.Navigation.OpenHomePage();
            if (!app.Contact.IsContactPresented())
            {
                AddContact(contact);
            }

            List<ContactData> oldContacts = app.Contact.GetContactList();
            oldContacts.Sort();
            DateTime edit_bdate = app.Contact.GenerateRandomDay();
            DateTime edit_adate = app.Contact.GenerateRandomDay();
            ContactData edit_contact = new ContactData { FirstName = contact.FirstName+"_edited", MiddleName = contact.MiddleName + "_edited", LastName = contact.LastName + "_edited", Nickname = contact.Nickname+ "_edited", Company = contact.Company+ "_edited", Title = contact.Title+"_edited", Address = contact.Address + "_edited", HomeTelephone = contact.HomeTelephone+"_edited", MobileTelephone = contact.MobileTelephone + "_edited", WorkTelephone = contact.WorkTelephone + "_edited", FaxTelephone = contact.FaxTelephone + "_edited", Email = contact.Email + "_edited", Email2 = contact.Email2 + "_edited", Email3 = contact.Email3 + "_edited", Homepage = contact.Homepage + "_edited", BirthdayDay = edit_bdate.Day.ToString(), BirthdayMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(edit_bdate.Month), BirthdayYear = edit_bdate.Year.ToString(), AnniversaryDay = edit_adate.Day.ToString(), AnniversaryMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(edit_adate.Month), AnniversaryYear = edit_adate.Year.ToString(), SecondaryAddress = contact.SecondaryAddress + "_edited", SecondaryHome = contact.SecondaryHome + "_edited", SecondaryNotes = contact.SecondaryNotes + "_edited" };
            edit_contact.Id = oldContacts[oldContacts.Count - 1].Id;

            app.Contact.EditLastCreatedContact(edit_contact);

            List<ContactData> newContacts = app.Contact.GetContactList();
            newContacts.Sort();

            Assert.AreEqual(edit_contact, newContacts[newContacts.Count - 1]);
            app.Contact.AssertContactValues(edit_contact);
        }

        [Test, TestCaseSource("ContactDataFromXmlFile")]
        public void DeleteContact(ContactData contact)
        {
            app.Navigation.OpenHomePage();

            if (!app.Contact.IsContactPresented())
            {
                AddContact(contact);
            }

            List<ContactData> oldContacts = app.Contact.GetContactList();
            oldContacts.Sort();
            string lcc_id = app.Contact.DeleteLastCreatedContact();

            app.Contact.AssertLastContactDeleted(lcc_id);
            int lccIndex = oldContacts.FindIndex(p => p.Id == Convert.ToInt32(lcc_id));
            List<ContactData> newContacts = app.Contact.GetContactList();
            newContacts.Sort();
            oldContacts.RemoveAt(lccIndex);
            Assert.AreEqual(newContacts, oldContacts);

        }
    }
}
