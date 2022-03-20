﻿// Generated by Selenium IDE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

namespace AddressBook
{

    [TestFixture]
    public class AddressBookTests : TestBase
    {
        [Test]
        public void AddContact()
        {
            ContactData contact = new ContactData { FirstName = "FirstName", MiddleName = "MiddleName", LastName = "LastName", Nickname = "Nickname", Company = "Company", Title = "Title", Address = "Address", HomeTelephone = "HomeTelephone", MobileTelephone = "MobileTelephone", WorkTelephone = "WorkTelephone", FaxTelephone = "FaxTelephone", Email = "Email", Email2 = "Email2", Email3 = "Email3", Homepage = "Homepage", BirthdayDay = "21", BirthdayMonth = "January", BirthdayYear = "1989", AnniversaryDay = "1", AnniversaryMonth = "January", AnniversaryYear = "2029", SecondaryAddress = "SecondaryAddress", SecondaryHome = "SecondaryHome", SecondaryNotes = "SecondaryNotes" };
            CreateContact(contact);
            AssertContactValues(contact);
        }

        [Test]
        public void AddGroup()
        {
            GroupData group = new GroupData("New Group") { Header = "sds", Footer = "dsfsd" };
            CreateGroup(group);
            AssertGroupCreated(group);
        }

        [Test]
        public void EditGroup()
        {
            if (!IsGroupPresented())
            {
                AddGroup();
            }

            GroupData group = new GroupData("New Group_edited") { Header = "sds_edited", Footer = "dsfsd_edited" };
            EditLastCreatedGroup(group);
        }

        [Test]
        public void DeleteGroup()
        {
            if (!IsGroupPresented())
            {
                AddGroup();
            }

            AssertLastCreatedGroupDeleted(DeleteLastCreatedGroup());
        }

        [Test]
        public void EditContact()
        {
            if (!IsContactPresented())
            {
                AddContact();
            }

            ContactData contact = new ContactData { FirstName = "FirstName_edited", MiddleName = "MiddleName_edited", LastName = "LastName_edited", Nickname = "Nickname_edited", Company = "Company_edited", Title = "Title_edited", Address = "Address_edited", HomeTelephone = "HomeTelephone_edited", MobileTelephone = "MobileTelephone_edited", WorkTelephone = "WorkTelephone_edited", FaxTelephone = "FaxTelephone_edited", Email = "Email_edited", Email2 = "Email2_edited", Email3 = "Email3_edited", Homepage = "Homepage_edited", BirthdayDay = "22", BirthdayMonth = "July", BirthdayYear = "1990", AnniversaryDay = "1", AnniversaryMonth = "January", AnniversaryYear = "2039", SecondaryAddress = "SecondaryAddress_edited", SecondaryHome = "SecondaryHome_edited", SecondaryNotes = "SecondaryNotes_edited" };
            EditLastCreatedContact(contact);
            AssertContactValues(contact);
        }

        [Test]
        public void DeleteContact()
        {
            if (!IsContactPresented())
            {
                AddContact();
            }

            AssertLastContactDeleted(DeleteLastCreatedContact());

        }
    }
}
