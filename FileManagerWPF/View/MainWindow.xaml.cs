﻿using System;
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
                        dc.path = diskName;
                        ComboBoxPath.SelectedValue = dc.path;
                    }
                    else
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(diskName);
                        if (directoryInfo.Exists)
                            dc.path = diskName;
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
                    dc.path = comboBox.Text;
                    ComboBoxPath.SelectedValue = dc.path;

                }
                else
                {
                    MessageBox.Show(this, "This path not exists", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            //dc.path = comboBox.SelectedValue.ToString();
        }

        private void ComboBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            dc.RefreshAllCommand.Execute(true);
        }

        private void DataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = (DataGrid) sender;
            FilesAndDirectories element = (FilesAndDirectories)grid.SelectedItem;
            if (element.Extension == "Directory")
            {
                dc.path = element.Path;
                ComboBoxPath.SelectedValue = dc.path;

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

        private void DiskTextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FilesAndDirectories textBox = (FilesAndDirectories) sender;
            dc.path = textBox.Path;
            
        }

    }
}