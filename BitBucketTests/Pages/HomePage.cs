using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BitBucketTests
{
    public class HomePage
    {
        public By CreateButton() => By.XPath("//button[@data-testid=\"create-button\"]");

        public By RepositoryButton() => By.XPath("//span[text()=\"Repository\"]");

        public By CreateRepo() => By.XPath("//a[@class=\"css-1kkpu7c\"]");

        public By SelectProject() => By.Id("s2id_id_project");

        public By CreateNewProject() => By.XPath("//span[text()=\"Create new project\"]");

        public By ProjectName() => By.Id("id_project_name");

        public By RepoName() => By.Name("name");

        public By SubmitRepo() => By.XPath("//button[@type=\"submit\"]");

        public By RepoNameOnProjectPage() => By.XPath("//div/div/h1");
    }
}
