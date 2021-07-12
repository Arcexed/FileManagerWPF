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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace View3DModel
{
    /// <summary>
    /// Interaction logic for View3DModelWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel m = new MainViewModel();
            this.DataContext = m;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Choose file

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "STP files (*.stp)|*.stp";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if ((bool)openFileDialog1.ShowDialog())
            {
                string selectedFileName = openFileDialog1.FileName;
                FilenameTextBox.Text = selectedFileName;
                //...
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string file = FilenameTextBox.Text;
            if (File.Exists(file))
            {
                View3DModelWindow view = new View3DModelWindow(file);
                view.Show();
            }
            else
            {
                MessageBox.Show("Not found file");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string file = FilenameTextBox.Text;
            if (File.Exists(file))
            {
                var thread = new Thread(ProgressBar);
                thread.Start();
            }
            else
            {
                MessageBox.Show("Not found file");

            }

        }

        private void ProgressBar()
        {
            Dispatcher.Invoke(() => Progressbar.Value = 0);
            Dispatcher.Invoke(() => PrintButton.IsEnabled = false);
            Dispatcher.Invoke(() => Progressbar.Visibility = Visibility.Visible);
            for (int i = 0; i < 20; i++)
            {
                Dispatcher.Invoke(() => Progressbar.Value += 5);
                Thread.Sleep(300);
            }
            Dispatcher.Invoke(() => PrintButton.IsEnabled = true);

        }
    }
}
