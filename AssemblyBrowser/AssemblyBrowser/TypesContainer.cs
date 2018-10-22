using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
	public abstract class TypesContainer
	{
		abstract public string Name { get; }

		abstract public IEnumerable<TypeInfo> TypesInfo { get; }

		abstract public IEnumerable<NamespaceInfo> Namespaces { get; }

		abstract internal NamespaceInfo GetNamespace(string name);

		abstract internal void AddType(TypeInfo typeInfo);
	}
}
