using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace BitBucketTests
{
    public class RepoHelper : HelperBase
    {
        public RepoHelper(ApplicationManager manager) : base( manager)
        {
        }

        protected internal void CreateRepository(RepoData repo)
        {
            WaitUntilVisible(HomePage.CreateButton());
            Click(HomePage.CreateButton());
            Click(HomePage.RepositoryButton());
            Click(HomePage.SelectProject());
            Click(HomePage.CreateNewProject());
            FillTheField(HomePage.ProjectName(), repo.ProjectName);
            FillTheField(HomePage.RepoName(), repo.RepoName);
            Click(HomePage.SubmitRepo());

        }

        protected internal string GetRepoName()
        {
            return driver.FindElement(HomePage.RepoNameOnProjectPage()).Text;
        }
    }
}
