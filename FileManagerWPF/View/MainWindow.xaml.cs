using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using FileManagerWPF.Models;

namespace FileManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = dc;
           
        }
        MainViewModel dc = new MainViewModel();

        private void DiskTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBox s = (TextBox)sender;
            string diskName = s.Text.Split('\t')[0];
            foreach (var drive in dc.DrivesList)
            {
                if (drive.Name == diskName)
                {
                    if (drive.Type != DriveType.CDRom.ToString())
                    {
                        dc.CurrentPath = diskName;
                        ComboBoxPath.SelectedValue = dc.CurrentPath;
                    }
                    else
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(diskName);
                        if (directoryInfo.Exists)
                            dc.CurrentPath = diskName;
                        else
                        {
                            dc.FilesAndDirectories.Clear();
                        }
                    }
                    return;
                }   
            }
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void ComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                
                ComboBox comboBox = (ComboBox) sender;
                if (Directory.Exists(comboBox.Text))
                {
                    dc.CurrentPath = comboBox.Text;
                    dc.RefreshPathComboBox(dc.CurrentPath);
                    ComboBoxPath.SelectedItem = dc.CurrentPath;


                }
                else
                {
                    MessageBox.Show(this, "This CurrentPath not exists", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ComboBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
        }

        private void DataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = (DataGrid) sender;
            FilesAndDirectories element = (FilesAndDirectories)grid.SelectedItem;
            if (element != null && element.Path!=null)
            {
                if (element.Extension == "Directory")
                {
                    dc.RefreshPathComboBox(element.Path);
                }
                else
                {
                    using (Process process = new Process())
                    {
                        process.StartInfo.UseShellExecute = true;
                        process.StartInfo.FileName = element.Path;
                        process.Start();
                    }
                }
            }

        }

        private void DiskTextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FilesAndDirectories textBox = (FilesAndDirectories) sender;
            dc.CurrentPath = textBox.Path;
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem filesAndDirectories = (MenuItem) sender;
            var ItemToDelete =(FilesAndDirectories)FilesAndDirectoriesDataGrid.SelectedCells.FirstOrDefault().Item;
            if (ItemToDelete != null)
            {
                try
                {
                    if (ItemToDelete.Extension.ToLower() == "directory")
                    {
                        Directory.Delete(ItemToDelete.Path);
                    }
                    else
                    {
                        File.Delete(ItemToDelete.Path);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                dc.RefreshAllCommand.Execute(true);
            }
        }
    }
}
