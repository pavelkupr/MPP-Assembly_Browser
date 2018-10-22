using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AssemblyBrowser
{
	public class AssemblyInfoGenerator
	{
		public AssemblyInfo GenerateAssemblyInfo(string path)
		{
			Assembly assembly = Assembly.LoadFrom(path);
			AssemblyInfo assemblyInfo = new AssemblyInfo(assembly.GetName().Name);
			Type[] types = assembly.GetTypes();
			foreach(Type type in types)
			{
				if (!type.IsNested)
				{
					TypesContainer namespaceInfo;
					if (type.Namespace != null)
					{
						string[] namespaces = type.Namespace.Split('.');
						namespaceInfo = assemblyInfo.GetNamespace(namespaces[0]);
						for (int i = 1; i < namespaces.Length; i++)
						{
							namespaceInfo = namespaceInfo.GetNamespace(namespaces[i]);
						}
					}
					else
						namespaceInfo = assemblyInfo;

					namespaceInfo.AddType(GenerateTypeInfo(type));
				}
			}
			return assemblyInfo;
		}

		private TypeInfo GenerateTypeInfo(Type type)
		{
			TypeInfo result = new TypeInfo(type.Name);
			foreach(FieldInfo info in type.GetFields())
			{
				result.AddField(info);
			}
			foreach (MethodInfo info in type.GetMethods())
			{
				result.AddMethod(info);
			}
			foreach (PropertyInfo info in type.GetProperties())
			{
				result.AddProperty(info);
			}
			return result;
		}
	}
}
