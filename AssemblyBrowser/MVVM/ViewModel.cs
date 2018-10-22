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
		private string currPath;
		private AssemblyInfoGenerator generator;
		private AssemblyInfo info;
		private TypeInfo selectedType;
		private Command updateCommand, nextCommand, backCommand;
		private TypesContainer selectedContainer, currContainer;
		private Stack<TypesContainer> stack;

		public Command UpdateCommand
		{
			get
			{
				return updateCommand ??
				  (updateCommand = new Command(obj =>
				  {
					  AssemblyInfo = generator.GenerateAssemblyInfo(currPath);
					  stack = new Stack<TypesContainer>();
					  stack.Push(AssemblyInfo);
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

		public string CurrPath
		{
			get { return currPath; }
			set
			{
				currPath = value;
				OnPropertyChanged("CurrPath");
			}
		}

		public TypeInfo SelectedType
		{
			get { return selectedType; }
			set
			{
				selectedType = value;
				OnPropertyChanged("SelectedType");
				
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