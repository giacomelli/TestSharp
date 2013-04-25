using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestSharp.Tests.Helpers;

namespace TestSharp.Tests
{
	[TestClass]
	public class VSProjectHelperTest
	{
		[TestMethod]
		public void GetProjectFolderPathTest()
		{
			var actualPath = VSProjectHelper.GetProjectFolderPath(WebSiteStubHelper.ProjectFolderName);

			Assert.IsNotNull(actualPath);
			Assert.IsTrue(actualPath.Length > 0);
			PathAssert.IsPathRooted(actualPath);
		}
	}
}
