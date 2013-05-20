using System.Net;
using NUnit.Framework;
using TestSharp.Tests.Helpers;

namespace TestSharp.Tests
{
	[TestFixture()]
	public class NetHelperTest
	{
		#region Initialize / Cleanup
		[SetUp]
		public void TestInitialize()
		{
			WebSiteStubHelper.StartAndWaitForResponse();
		}

		[TearDown]
		public void TestCleanup()
		{
			WebSiteStubHelper.Stop();
		}
		#endregion

		#region Tests
		[Test]
		public void GetContentTest()
		{			
			var actual = NetHelper.GetContent(WebSiteStubHelper.VirtualPath);

			StringAssert.Contains("My ASP.NET Application", actual);
			StringAssert.Contains("documentation on ASP.NET at MSDN", actual);			
		}

		[Test]
		public void IsRedirectedTest()
		{
			var actual = NetHelper.IsRedirected(WebSiteStubHelper.VirtualPath + "/Default.aspx");
			Assert.IsFalse(actual, "Default.aspx não é redirecionada.");

			actual = NetHelper.IsRedirected(WebSiteStubHelper.VirtualPath + "/Moved.aspx");
			Assert.IsTrue(actual, "Moved.aspx é redirecionada.");
		}

		[Test]
		public void RequestTest()
		{			
			NetHelper.GetContent(WebSiteStubHelper.VirtualPath);

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

		[Test]
		public void IsRespondingTest()
		{
			Assert.IsTrue(NetHelper.IsResponding(WebSiteStubHelper.VirtualPath));
		}
		#endregion
	}
}
