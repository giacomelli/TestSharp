using NUnit.Framework;
using TestSharp.Tests.Helpers;

namespace TestSharp.Tests
{
	[TestFixture()]
	public class VSProjectHelperTest
	{
		[Test]
		public void GetProjectFolderPathTest()
		{
			var actualPath = VSProjectHelper.GetProjectFolderPath(WebSiteStubHelper.ProjectFolderName);

			Assert.IsNotNull(actualPath);
			Assert.IsTrue(actualPath.Length > 0);
			PathAssert.IsPathRooted(actualPath);
		}
	}
}
