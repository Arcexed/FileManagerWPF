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
using RelayCommand = GalaSoft.MvvmLight.CommandWpf.RelayCommand;
namespace FileManagerWPF
{
    class MainViewModel
    {
        public MainViewModel()
        {
            ExecuteRefreshAll();
            RefreshAllCommand = new RelayCommand(ExecuteRefreshAll);
        }

        public ObservableCollection<Files> FilesList { get; set; }= new ObservableCollection<Files>();
        public ObservableCollection<Directories> DirectoriesList { get; set; } = new ObservableCollection<Directories>();
        public ObservableCollection<Drive> DrivesList { get; set; } = new ObservableCollection<Drive>();

        public ObservableCollection<FilesAndDirectories> FilesAndDirectories { get; set; } =
            new ObservableCollection<FilesAndDirectories>();



        private string _path { get; set; } = @"C:\";
        public string path
        {
            get => _path;
            set
            {
                if (path != value)
                {
                    _path = value;
                }
                RefreshAllCommand.Execute(true);



            }
        }


        // GET DIRECTORIES







        public string[] PathComboBox
        {
            get
            {
                string[] list = Directory.GetDirectories(path);
                List<string> tempList = new List<string>();
                foreach (var temp in list)
                {
                    if (!temp.Contains($"$"))
                    {
                        tempList.Add(temp);
                    }   
                }
                return tempList.ToArray();
            }
        }

       
        // GET DRIVES
        public ICommand RefreshAllCommand { get; private set; }
        private void ExecuteRefreshAll()
        {
            RefreshDrives();
            RefreshDirectories();
            RefreshFiles();
            RefreshDirectoriesAndFiles();
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

                    PersentUsedDiskSpace = Convert.ToInt32(Math.Round((double)(drive.TotalSize - drive.TotalFreeSpace) /
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
            
            List<string> list = Directory.GetFiles(path).ToList();
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
            try
            {
                List<string> list = Directory.GetDirectories(path).ToList();
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
            catch (ArgumentException ex)
            {
                MessageBox.Show("Invaild path", "Error", MessageBoxButton.OK,MessageBoxImage.Hand);
            }
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
    }
}

