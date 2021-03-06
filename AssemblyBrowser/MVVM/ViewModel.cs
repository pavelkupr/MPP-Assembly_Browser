﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using AssemblyBrowser;
namespace MVVM
{
	class ViewModel : INotifyPropertyChanged
	{
		private string currPath, errorMsg;
		private AssemblyInfoGenerator generator;
		private AssemblyInfo info;
		private TypeInfo selectedType;
		private Command updateCommand, nextCommand, backCommand;
		private TypesContainer selectedContainer, currContainer;
		private Stack<TypesContainer> stack;
		private bool isFullTypeName;

		public Command UpdateCommand
		{
			get
			{
				return updateCommand ??
				  (updateCommand = new Command(obj =>
				  {
					  try
					  {
						  generator.isFullTypeName = isFullTypeName;
						  AssemblyInfo curr = generator.GenerateAssemblyInfo(currPath);
						  AssemblyInfo = curr;
						  stack = new Stack<TypesContainer>();
						  stack.Push(AssemblyInfo);
						  OnPropertyChanged("CurrAssemblyPath");
						  ErrorMsg = "";
					  }
					  catch
					  {
						  ErrorMsg = "Невозможно загрузить сборку";
					  }
				  }));
			}
		}
		public Command NextCommand
		{
			get
			{
				return nextCommand ??
				  (nextCommand = new Command(obj =>
				  {
					  if (currContainer != null)
					  {
						  SelectedContainer = currContainer;
						  stack.Push(SelectedContainer);
						  SelectedType = null;
						  currContainer = null;
						  OnPropertyChanged("CurrAssemblyPath");
					  }
				  }));
			}
		}
		public Command BackCommand
		{
			get
			{
				return backCommand ??
				  (backCommand = new Command(obj =>
				  {
					  if (stack.Count > 1)
					  {
						  stack.Pop();
						  SelectedContainer = stack.Peek();
						  SelectedType = null;
						  currContainer = null;
						  OnPropertyChanged("CurrAssemblyPath");
					  }
				  }));
			}
		}
		public AssemblyInfo AssemblyInfo
		{
			get { return info; }
			set
			{
				info = value;
				SelectedContainer = value;
				OnPropertyChanged("AssemblyInfo");
			}
		}

		public bool IsFullTypeName
		{
			get { return isFullTypeName; }
			set
			{
				isFullTypeName = value;
				OnPropertyChanged("IsFullTypeName");
			}
		}

		public string CurrPath
		{
			get { return currPath; }
			set
			{
				currPath = value;
				OnPropertyChanged("CurrPath");
			}
		}
		public string ErrorMsg
		{
			get { return errorMsg; }
			set
			{
				errorMsg = value;
				OnPropertyChanged("ErrorMsg");
			}
		}
		public string CurrAssemblyPath
		{
			get
			{
				string result="";
				if (AssemblyInfo != null)
				{
					TypesContainer[] containers = stack.ToArray();
					for (int i = containers.Length-1; i >= 0; i--)
					{
						if (i == containers.Length - 1)
							result += containers[i].Name;
						else if (i == containers.Length - 2)
							result += ": " + containers[i].Name;
						else
							result += "." + containers[i].Name;
					}
					if (selectedType != null)
						result += "." + selectedType.Name;
				}
				return result;
			}
		}
		public TypeInfo SelectedType
		{
			get { return selectedType; }
			set
			{
				selectedType = value;
				OnPropertyChanged("SelectedType");
				OnPropertyChanged("CurrAssemblyPath");
			}
		}

		public TypesContainer SelectedContainer
		{
			get { return selectedContainer; }
			set
			{
				if (value != null)
				{
					selectedContainer = value;
					OnPropertyChanged("SelectedContainer");
				}
			}
		}

		public TypesContainer CurrContainer
		{
			get { return currContainer; }
			set
			{
				if (value != null)
				{
					currContainer = value;
					OnPropertyChanged("CurrContainer");
				}
			}
		}

		public ViewModel()
		{
			generator = new AssemblyInfoGenerator();
			currPath = @"D:\GitHub\MPP-Faker\MPP2\FakerLib\bin\Debug\FakerLib.dll";
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
