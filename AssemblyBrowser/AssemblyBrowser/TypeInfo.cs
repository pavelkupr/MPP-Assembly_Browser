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
		private bool mode;
		private readonly List<string> methods;
		public IEnumerable<string> Methods { get { return methods; } }

		private readonly List<string> fields;
		public IEnumerable<string> Fields { get { return fields; } }

		private readonly List<string> properties;
		public IEnumerable<string> Properties { get { return properties; } }

		internal TypeInfo(string _name, bool isFullTypeName)
		{
			Name = _name;
			OnPropertyChanged("Name");
			mode = isFullTypeName;
			methods = new List<string>();
			fields = new List<string>();
			properties = new List<string>();
		}

		internal void AddMethod(MethodInfo info)
		{
			if (info.IsSpecialName == true)
				return;
			string result;
			result = TypeNameFormat(info.ReturnType)+ " "+ info.Name + "(";
			ParameterInfo[] parameters = info.GetParameters();
			for (int i = 0;i< parameters.Length;i++)
			{
				if (i != 0)
					result += ", ";
				result += TypeNameFormat(parameters[i].ParameterType);
			}
			result +=")";
			methods.Add(result);
			OnPropertyChanged("Methods");
		}

		internal void AddField(FieldInfo info)
		{
			string result;
			result = TypeNameFormat(info.FieldType)+ " " + info.Name;
			fields.Add(result);
			OnPropertyChanged("Fields");
		}

		internal void AddProperty(PropertyInfo info)
		{
			string result;
			result = TypeNameFormat(info.PropertyType) + " " + info.Name;
			properties.Add(result);
			OnPropertyChanged("Properties");
		}

		private string TypeNameFormat(Type type)
		{
			string result;
			if (!mode)
			{
				if (type.IsGenericType)
				{
					result = type.GetGenericTypeDefinition().Name;
					result = result.Remove(result.Length-2,2);
					result += "<" + type.GetGenericArguments()[0].Name;
					for (int i = 1; i < type.GetGenericArguments().Length; i++)
					{
						result += ", " + type.GetGenericArguments()[i].Name;
					}
					result += ">";
				}
				else
					result = type.Name;
			}
			else
			{
				if (type.IsGenericType)
				{
					result = type.GetGenericTypeDefinition().FullName;
					result = result.Remove(result.Length - 2, 2);
					result += "<" + type.GetGenericArguments()[0].FullName;
					for (int i = 1; i < type.GetGenericArguments().Length; i++)
					{
						result += ", " + type.GetGenericArguments()[i].FullName;
					}
					result += ">";
				}
				else
					result = type.FullName;
			}
			return result;
		}
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
