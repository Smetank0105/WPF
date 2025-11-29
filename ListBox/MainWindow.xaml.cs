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
using System.IO;

namespace ListBox
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			string[] files = Directory.EnumerateFiles("C:\\Windows").ToArray();
			foreach (string file in files)
			{
				listBox.Items.Add(file);
			}
		}

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			if(!listBox.Items.Contains(txtInput.Text))
				listBox.Items.Add(txtInput.Text);
		}

		private void btnDel_Click(object sender, RoutedEventArgs e)
		{
			while(listBox.SelectedItems.Count > 0)
				listBox.Items.Remove(listBox.SelectedItem);
		}

		private void btnClr_Click(object sender, RoutedEventArgs e)
		{
			listBox.Items.Clear();
		}
	}
}
