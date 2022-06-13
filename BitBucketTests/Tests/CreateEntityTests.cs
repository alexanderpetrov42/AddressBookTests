using BitBucketTests.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace BitBucketTests
{

    [TestFixture]
    public class CreateEntityTests : AuthBase
    {
        public static IEnumerable<RepoData> RepoDataFromXmlFile()
        {
            return (List<RepoData>)new XmlSerializer(typeof(List<RepoData>))
                .Deserialize(new StreamReader(@"repos.xml"));
        }

        [Test, TestCaseSource("RepoDataFromXmlFile")]
        public void CreateRepo(RepoData repo)
        {
            app.Repo.CreateRepository(repo);
            Assert.AreEqual(repo.RepoName, app.Repo.GetRepoName());
        }
    }
}
