using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FileManagerWPF.Models;
using GalaSoft.MvvmLight;
using RelayCommand = GalaSoft.MvvmLight.CommandWpf.RelayCommand;

namespace FileManagerWPF
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            ExecuteRefreshAll();
            RefreshAllCommand = new RelayCommand(ExecuteRefreshAll);
            UpToDirectoryCommand = new RelayCommand(ExecuteUpToDirectory);
        }

        public ObservableCollection<Files> FilesList { get; set; } = new ObservableCollection<Files>();

        public ObservableCollection<Directories> DirectoriesList { get; set; } =
            new ObservableCollection<Directories>();

        public ObservableCollection<Drive> DrivesList { get; set; } = new ObservableCollection<Drive>();

        public ObservableCollection<FilesAndDirectories> FilesAndDirectories { get; set; } =
            new ObservableCollection<FilesAndDirectories>();

        public const string defpath = @"C:\";

        private string _path = defpath;

        public string CurrentPath
        {
            get => _path;
            set
            {
                _path = value;
                RaisePropertyChanged();
                ExecuteRefreshAll();
            }
        }

        // GET DIRECTORIES


        private List<string> _pathComboBox = new List<string>();

        public List<string> PathComboBox
        {
            get { return _pathComboBox; }
            set { _pathComboBox = value; }
        }

        public void RefreshPathComboBox(string pth = null)
        {
            PathComboBox = new List<string>();
            var directory = DirectoriesList;
            foreach (var temp in directory)
            {

                PathComboBox.Add(temp.Path);

            }
            if (pth != null)
            {
                var index = PathComboBox.IndexOf(pth);
                if (index >= 0)
                {
                    CurrentPath = PathComboBox[index];
                }
            }

        }

        // GET DRIVES
        public ICommand RefreshAllCommand { get; private set; }

        private void ExecuteRefreshAll()
        {
            try
            {
                RefreshDrives();
                RefreshDirectories();
                RefreshFiles();
                RefreshPathComboBox();
                RefreshDirectoriesAndFiles();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Invaild CurrentPath", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Don't have access", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                DirectoryInfo info = new DirectoryInfo(_path);
                if (info.Parent.FullName != null)
                {
                    CurrentPath = info.Parent.FullName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void RefreshDrives()
        {
            DrivesList.Clear();
            foreach (var drive in DriveInfo.GetDrives())
            {
                string Name = drive.Name;
                string Type = drive.DriveType.ToString();
                int PersentUsedDiskSpace = 0;
                if (drive.DriveType == DriveType.Fixed || drive.DriveType == DriveType.Removable)
                {
                    PersentUsedDiskSpace = Convert.ToInt32(Math.Round(
                        (double)(drive.TotalSize - drive.TotalFreeSpace) /
                        drive.TotalSize * 100));
                }
                else
                {
                }

                DrivesList.Add(new Drive()
                {
                    Name = Name,
                    Type = Type,
                    PersentUsedDiskSpace = PersentUsedDiskSpace
                });
            }
        }

        private void RefreshFiles()
        {
            FilesList.Clear();

            List<string> list = Directory.GetFiles(string.IsNullOrEmpty(CurrentPath) ? defpath : CurrentPath).ToList();
            var filesCollection = new ObservableCollection<Files>();
            foreach (var element in list)
            {
                filesCollection.Add(new Files()
                {
                    Path = element,
                    Name = element.Split('\\').Last()
                });
            }

            FilesList = filesCollection;
        }

        private void RefreshDirectories()
        {
            DirectoriesList.Clear();

            List<string> list = Directory.GetDirectories(string.IsNullOrEmpty(CurrentPath) ? defpath : CurrentPath).ToList();
            var directoryCollection = new ObservableCollection<Directories>();
            foreach (var element in list)
            {
                directoryCollection.Add(new Directories()
                {
                    Path = element,
                    Name = element.Split('\\').Last(),
                });
            }


            DirectoriesList = directoryCollection;
        }

        private void RefreshDirectoriesAndFiles()
        {
            FilesAndDirectories.Clear();
            foreach (var directory in DirectoriesList)
            {
                DirectoryInfo info = new DirectoryInfo(directory.Path);
                FilesAndDirectories.Add(new FilesAndDirectories()
                {
                    Name = info.Name,
                    Path = info.FullName,
                    Extension = "Directory",
                    DateEdited = info.LastWriteTime.ToShortDateString()
                });
            }

            foreach (var file in FilesList)
            {
                FileInfo info = new FileInfo(file.Path);
                FilesAndDirectories.Add(new FilesAndDirectories()
                {
                    Name = info.Name,
                    Path = info.FullName,
                    Extension = info.Extension,
                    DateEdited = info.LastWriteTime.ToShortDateString(),
                    Size = Math.Round((double)(info.Length / 1000)).ToString() + " Kbyte"
                });
            }
        }

        public ICommand UpToDirectoryCommand { get; private set; }

        private void ExecuteUpToDirectory()
        {
            DirectoryInfo directory = new DirectoryInfo(CurrentPath);
            if (directory.Parent != null)
            {
                this.CurrentPath = directory.Parent.FullName;
            }
        }
    }
}