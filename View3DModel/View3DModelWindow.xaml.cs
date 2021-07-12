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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HelixToolkit.Wpf;

namespace View3DModel
{
    /// <summary>
    /// Interaction logic for View3DModelWindow.xaml
    /// </summary>
    public partial class View3DModelWindow : Window
    {
        public View3DModelWindow(string path)
        {
            InitializeComponent();
            string filename = path;

            ModelVisual3D device3D = new ModelVisual3D();
            device3D.Content = Display3d(filename);
            myView.Children.Add(device3D);
        }

        private Model3D Display3d(string model)
        {
            Model3D device = null;
            try
            {
                myView.RotateGesture = new MouseGesture(MouseAction.LeftClick);
                ModelImporter import = new ModelImporter();
                device = import.Load(model);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Error : " + e.StackTrace);
            }
            return device;
        }
    }
    
}
