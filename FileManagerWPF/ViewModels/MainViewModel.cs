using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using FileManagerWPF.Models;
using RelayCommand = GalaSoft.MvvmLight.CommandWpf.RelayCommand;
namespace FileManagerWPF
{
    class MainViewModel
    {
        public MainViewModel()
        {
            RefreshDrivesCommand = new RelayCommand(ExecuteRefreshDrives);
            reloadDisksThread = new Thread(ReloadDrives);
            reloadDisksThread.Start();
        }

        public Thread reloadDisksThread;
        public ObservableCollection<Files> FilesList
        {
            get
            {
                List<string> list = Directory.GetFiles(path).ToList();
                var filesCollection = new ObservableCollection<Files>();
                foreach (var element in list)
                {
                    filesCollection.Add(new Files()
                    {
                        Path=element,
                        Name=element.Split('\\').Last()
                    });
                }
                return filesCollection;
            }
        }
        public ObservableCollection<FilesAndDirectories> FilesAndDirectories
        {
            get
            {
                ObservableCollection<FilesAndDirectories> all = new ObservableCollection<FilesAndDirectories>();
                foreach (var directory in DirectoriesList)
                {
                    DirectoryInfo info = new DirectoryInfo(directory.Path);
                    all.Add(new FilesAndDirectories()
                    {
                         Name=info.Name,
                         Path=info.FullName,
                         Extension = "Directory",
                         DateEdited = info.LastWriteTime.ToLongDateString()
                    });
                }
                foreach (var file in FilesList)
                {
                    DirectoryInfo info = new DirectoryInfo(file.Path);
                    all.Add(new FilesAndDirectories()
                    {
                        Name = info.Name,
                        Path=info.FullName,
                        Extension = info.Extension,
                        DateEdited = info.LastWriteTime.ToLongDateString()
                    });
                }
                return all;
            }
        }
        //private ObservableCollection<Drive> _drivesList;

        //public ObservableCollection<Drive> DrivesList
        //{
        //    get => _drivesList;
        //    set => _drivesList = value;
        //}

        //можешь делать так и так
        // если допальнительная логика не будет в геттере или сеттере то есть синтаксис короче
        //они то же самое
        private void ReloadDrives()
        {
            while (true)
            {
                ObservableCollection<Drive> drivesCollection = new ObservableCollection<Drive>();
                foreach (var drive in DriveInfo.GetDrives())
                {
                    string Name = drive.Name;
                    string Type = drive.DriveType.ToString();
                    int PersentUsedDiskSpace = 0;
                    if (drive.DriveType == DriveType.Fixed || drive.DriveType==DriveType.Removable)
                    {
                        
                        PersentUsedDiskSpace = Convert.ToInt32(Math.Round((double)(drive.TotalSize - drive.TotalFreeSpace) /
                            drive.TotalSize * 100));
                    }
                    else
                    {
                        
                    }
                    drivesCollection.Add(new Drive()
                    {
                        Name = Name,
                        Type = Type,
                        PersentUsedDiskSpace = PersentUsedDiskSpace
                    });
                    
                }
                if (drivesCollection != DrivesList)
                {
                    
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        drivesList.Clear();
                        drivesList = drivesCollection;
                    });
                }
                Thread.Sleep(5000);
            }
        }

        private ObservableCollection<Drive> drivesList = new ObservableCollection<Drive>();

        public ObservableCollection<Drive> DrivesList
        {
            get => drivesList;
            set => drivesList = value;
        }



        public static string path { get; set; } = @"C:\";

        // GET DIRECTORIES
        public ObservableCollection<Directories> DirectoriesList
        {
            get
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
                return directoryCollection;
            }
        }






        public static string[] PathComboBox
        {
            get
            {
                string[] list = Directory.GetDirectories(path);
                return list;
            }
        }

       
        // GET DRIVES
        public ICommand RefreshDrivesCommand { get; private set; }
        private void ExecuteRefreshDrives()
        {
            DrivesList.Clear();
            //foreach (var drive in DriveInfo.GetDrives())
            //{
            //    if (drive.DriveType == DriveType.Fixed)
            //    {
            //        DrivesList.Add(

            //            new Drive()
            //            {
            //                Name = drive.Name,
            //                Type = drive.DriveType.ToString(),
            //                PersentUsedDiskSpace = (double)(drive.TotalSize - drive.TotalFreeSpace) / drive.TotalSize * 100
            //            });
            //    }
            //    else if (drive.DriveType == DriveType.Removable)
            //    {
            //        DrivesList.Add(

            //            new Drive()
            //            {
            //                Name = drive.Name,
            //                Type = drive.DriveType.ToString(),
            //                PersentUsedDiskSpace = (double)(drive.TotalSize - drive.TotalFreeSpace) / drive.TotalSize * 100
            //            });
            //    }
            //    else
            //    {
            //        DrivesList.Add(

            //            new Drive()
            //            {
            //                Name = drive.Name,
            //                Type = drive.DriveType.ToString()
            //            });
            //    }

            //}
        }
    }
}

