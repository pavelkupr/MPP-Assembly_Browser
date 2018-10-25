using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			DataContext = new ViewModel();
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if(namespase_path.Text == "")
			{
				namespaces.Text = "";
				types.Text = "";
				methods.Text = "";
				properties.Text = "";
				fields.Text = "";
			}
			else
			{
				namespaces.Text = "Namespaces";
				types.Text = "Types";
				methods.Text = "Methods";
				properties.Text = "Properties";
				fields.Text = "Fields";
			}
		}
	}
}
