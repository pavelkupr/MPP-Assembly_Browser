using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

using System.Runtime.CompilerServices;
namespace AssemblyBrowser
{
	public class AssemblyInfo : TypesContainer, INotifyPropertyChanged
	{
		public override string Name { get; }

		private readonly List<TypeInfo> typesInfo;
		public override IEnumerable<TypeInfo> TypesInfo { get { return typesInfo; } }

		private readonly List<NamespaceInfo> namespaces;
		public override IEnumerable<NamespaceInfo> Namespaces { get { return namespaces; } }

		internal AssemblyInfo(string _name)
		{
			Name = _name;
			OnPropertyChanged("Name");

			typesInfo = new List<TypeInfo>();
			namespaces = new List<NamespaceInfo>();
		}

		internal override NamespaceInfo GetNamespace(string name)
		{
			NamespaceInfo result;

			if ((result = namespaces.Find(x => x.Name == name)) != null)
				return result;
			result = new NamespaceInfo(name);
			namespaces.Add(result);
			OnPropertyChanged("Namespaces");
			return result;
		}

		internal override void AddType(TypeInfo typeInfo)
		{
			typesInfo.Add(typeInfo);
			OnPropertyChanged("TypesInfo");
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
