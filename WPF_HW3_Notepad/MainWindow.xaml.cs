using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WPF_HW3_Notepad
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string currentFilePath = string.Empty;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void btnNew_Click(object sender, RoutedEventArgs e)
		{
			txtEditor.Text = string.Empty;
			currentFilePath = string.Empty;
			Title = "Notepad - New file";
		}

		private void btnOpen_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog
			{
				Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
			};
			if(ofd.ShowDialog()==true)
			{
				currentFilePath = ofd.FileName;
				txtEditor.Text = File.ReadAllText(currentFilePath);
				Title = $"Notepad - {System.IO.Path.GetFileName(currentFilePath)}";
			}
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			if(string.IsNullOrEmpty(currentFilePath))
				btnSaveAs_Click(sender, e);
			else
			{
				File.WriteAllText(currentFilePath, txtEditor.Text);
				MessageBox.Show("File saved!", "Saving", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void btnSaveAs_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog
			{
				Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
			};
			if(sfd.ShowDialog()==true)
			{
				currentFilePath = sfd.FileName;
				File.WriteAllText (currentFilePath, txtEditor.Text);
				Title = $"Notepad - {System.IO.Path.GetFileName(currentFilePath)}";
				MessageBox.Show("File saved!", "Saving", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
