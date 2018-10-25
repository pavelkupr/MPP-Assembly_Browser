using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyBrowser;

namespace AssemblyBrowserTests
{
	[TestClass]
	public class AssemblyInfoGeneratorTests
	{
		private static AssemblyInfo assemblyInfo;

		[ClassInitialize]
		public static void ClassInitilize(TestContext context)
		{
			AssemblyInfoGenerator generator = new AssemblyInfoGenerator();
			assemblyInfo = generator.GenerateAssemblyInfo("AssemblyBrowserTests.dll");
		}

		[TestMethod]
		public void AssemblyInfoGenerator_Generate_AssemblyInfo()
		{
			Assert.IsNotNull(assemblyInfo);
		}
		[TestMethod]
		public void AssemblyInfo_Has_correct_Name()
		{
			if(assemblyInfo.Name != "AssemblyBrowserTests")
				Assert.Fail("Name of assembly is incorrect");
		}
		[TestMethod]
		public void AssemblyInfo_Has_Right_Namespaces()
		{
			if( assemblyInfo.Namespaces.ToArray()[0].Name != "AssemblyBrowserTests")
				Assert.Fail("Name of namespace is incorrect");
		}

		[TestMethod]
		public void Namespace_Has_Right_Type()
		{
			NamespaceInfo namespaceInfo = assemblyInfo.Namespaces.ToArray()[0];

			if (namespaceInfo.TypesInfo.ToArray()[0].Name != "AssemblyInfoGeneratorTests")
				Assert.Fail("Name of type is incorrect");
		}
	}
}
