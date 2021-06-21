using System;
using System.Collections.Generic;
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
            dc.path = diskName;

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
                    dc.RefreshAllCommand.Execute(true);
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
            dc.path = comboBox.SelectedValue.ToString();
        }

        private void ComboBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }
    }
}
