using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestSharp.Tests.Helpers;

namespace TestSharp.Tests
{
	[TestClass]
	public class NetHelperTest
	{
		#region Initialize / Cleanup
		[TestInitialize]
		public void TestInitialize()
		{
			WebSiteStubHelper.Start();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			WebSiteStubHelper.Stop();
		}
		#endregion

		#region Tests
		[TestMethod]
		public void GetContentTest()
		{			
			var actual = NetHelper.GetContent(WebSiteStubHelper.VirtualPath);

			StringAssert.Contains(actual, "My ASP.NET Application");
			StringAssert.Contains(actual, "documentation on ASP.NET at MSDN");			
		}

		[TestMethod]
		public void IsRedirectedTest()
		{
			var actual = NetHelper.IsRedirected(WebSiteStubHelper.VirtualPath + "/Default.aspx");
			Assert.IsFalse(actual, "Default.aspx não é redirecionada.");

			actual = NetHelper.IsRedirected(WebSiteStubHelper.VirtualPath + "/Moved.aspx");
			Assert.IsTrue(actual, "Moved.aspx é redirecionada.");
		}

		[TestMethod]
		public void RequestTest()
		{			
			var actual = NetHelper.GetContent(WebSiteStubHelper.VirtualPath);

			FlowAssert.IsAtLeastOneOk(
			() => 
			{
				ExceptionAssert.IsThrowing(new WebException("O servidor remoto retornou um erro: (404) Não Localizado."), () =>
				{
					NetHelper.Request(WebSiteStubHelper.VirtualPath + "/essaPaginaNaoExisteOuNaoExistia");
				});
			},
			() => 
			{
				ExceptionAssert.IsThrowing(new WebException("The remote server returned an error: (404) Not Found."), () =>
				{
					NetHelper.Request(WebSiteStubHelper.VirtualPath + "/essaPaginaNaoExisteOuNaoExistia");
				});
			});
		}

		[TestMethod]
		public void IsRespondingTest()
		{
			Assert.IsTrue(NetHelper.IsResponding(WebSiteStubHelper.VirtualPath));
		}
		#endregion
	}
}
