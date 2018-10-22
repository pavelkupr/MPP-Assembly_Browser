using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using System.ComponentModel;

using System.Runtime.CompilerServices;
namespace AssemblyBrowser
{
	public class TypeInfo : INotifyPropertyChanged
	{
		public string Name { get; }

		private readonly List<string> methods;
		public IEnumerable<string> Methods { get { return methods; } }

		private readonly List<string> fields;
		public IEnumerable<string> Fields { get { return fields; } }

		private readonly List<string> properties;
		public IEnumerable<string> Properties { get { return properties; } }

		internal TypeInfo(string _name)
		{
			Name = _name;
			OnPropertyChanged("Name");

			methods = new List<string>();
			fields = new List<string>();
			properties = new List<string>();
		}

		internal void AddMethod(MethodInfo info)
		{
			string result;
			result = info.ReturnType.ToString()+" "+ info.Name + "(";
			ParameterInfo[] parameters = info.GetParameters();
			for (int i = 0;i< parameters.Length;i++)
			{
				if (i != 0)
					result += ", ";
				result += parameters[i].ParameterType.ToString();
			}
			result +=")";
			methods.Add(result);
			OnPropertyChanged("Methods");
		}

		internal void AddField(FieldInfo info)
		{
			string result;
			result = info.FieldType.ToString() + " " + info.Name;
			fields.Add(result);
			OnPropertyChanged("Fields");
		}

		internal void AddProperty(PropertyInfo info)
		{
			string result;
			result = info.PropertyType.ToString() + " " + info.Name;
			properties.Add(result);
			OnPropertyChanged("Properties");
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
