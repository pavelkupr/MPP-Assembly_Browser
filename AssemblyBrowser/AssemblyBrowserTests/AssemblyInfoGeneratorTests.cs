using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyBrowser;

namespace AssemblyBrowserTests
{
	[TestClass]
	public class AssemblyInfoGeneratorTests
	{
		public string testField;
		public string TestProperty { get; }

		private static AssemblyInfo assemblyInfo;
		private static NamespaceInfo namespaceInfo;
		private static TypeInfo typeInfo;

		[ClassInitialize]
		public static void ClassInitilize(TestContext context)
		{
			AssemblyInfoGenerator generator = new AssemblyInfoGenerator();
			generator.isFullTypeName = true;
			assemblyInfo = generator.GenerateAssemblyInfo("AssemblyBrowserTests.dll");
			foreach(NamespaceInfo nInfo in assemblyInfo.Namespaces)
			{
				if (nInfo.Name == "AssemblyBrowserTests")
				{
					namespaceInfo = nInfo;
					foreach (TypeInfo tInfo in nInfo.TypesInfo)
					{
						if(tInfo.Name == "AssemblyInfoGeneratorTests")
						{
							typeInfo = tInfo;
							break;
						}
					}
					break;
				}
			}
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
			Assert.IsNotNull(namespaceInfo, "AssemblyInfo doesn't have right NamespaseInfo");
		}

		[TestMethod]
		public void Namespace_Has_Right_Type()
		{
			Assert.IsNotNull(typeInfo, "NamespaseInfo doesn't have right TypeInfo");
		}

		[TestMethod]
		public void TypeInfo_Has_Right_Method()
		{
			bool isFinded = false;
			foreach(string method in typeInfo.Methods)
			{
				if (method == "System.Void TypeInfo_Has_Right_Method()")
					isFinded = true;
			}
			Assert.IsTrue(isFinded, "TypeInfo doesn't have right method");
		}

		[TestMethod]
		public void TypeInfo_Has_Right_Field()
		{
			bool isFinded = false;
			foreach (string field in typeInfo.Fields)
			{
				if (field == "System.String testField")
					isFinded = true;
			}
			Assert.IsTrue(isFinded, "TypeInfo doesn't have right field");
		}

		[TestMethod]
		public void TypeInfo_Has_Right_Property()
		{
			bool isFinded = false;
			foreach (string property in typeInfo.Properties)
			{
				if (property == "System.String TestProperty")
					isFinded = true;
			}
			Assert.IsTrue(isFinded, "TypeInfo doesn't have right field");
		}
	}
}
