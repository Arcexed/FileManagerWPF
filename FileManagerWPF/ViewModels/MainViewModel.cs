using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FileManagerWPF.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RelayCommand = GalaSoft.MvvmLight.CommandWpf.RelayCommand;

namespace FileManagerWPF
{
    class MainViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public MainViewModel()
        {
            ExecuteRefreshAll();
            CopyPathCommand = new RelayCommand(ExecuteCopyPath);
            PastePathCommand = new RelayCommand(ExecutePastePath);
            CutPathCommand = new RelayCommand(ExecuteCutPath);
            RefreshAllCommand = new RelayCommand(ExecuteRefreshAll);
            UpToDirectoryCommand = new RelayCommand(ExecuteUpToDirectory);
        }
        public ObservableCollection<Drive> DrivesList { get; set; } = new ObservableCollection<Drive>();

        public ObservableCollection<FilesAndDirectories> FilesAndDirectories { get; set; } =
            new ObservableCollection<FilesAndDirectories>();


        private string _path = @"C:\";

        public string CurrentPath
        {
            get => _path;
            set
            {
                _path = value;
                ExecuteRefreshAll();
            }
        }

        // GET DIRECTORIES


        private List<string> _pathComboBox = new List<string>();

        public List<string> PathComboBox
        {
            get
            {   
                return _pathComboBox;
            }
            set
            {
                _pathComboBox = value;
                RefreshPathComboBox();
            }
        }

        public void RefreshPathComboBox(string pth = null)
        {
            PathComboBox.Clear();
            PathComboBox.Add(CurrentPath);
            var directory = FilesAndDirectories.Where(f => f.Extension.ToLower()=="directory");
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
                RefreshDirectoriesAndFiles();
                RefreshPathComboBox();

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


                DrivesList.Add(new Drive()
                {
                    Name = Name,
                    Type = Type,
                    PersentUsedDiskSpace = PersentUsedDiskSpace
                });
            }
        }

      



        private void RefreshDirectoriesAndFiles()
        {
            FilesAndDirectories.Clear();
            List<string> directoryList = Directory.GetDirectories(CurrentPath).ToList();
            foreach (var element in directoryList)
            {
                DirectoryInfo info = new DirectoryInfo(element);
                FilesAndDirectories.Add(new FilesAndDirectories()
                {
                    Name = info.Name,
                    Path = info.FullName,
                    Extension = "Directory",
                    DateEdited = info.LastWriteTime.ToShortDateString()
                });
            }

            List<string> fileList = Directory.GetFiles(CurrentPath).ToList();
            foreach (var file in fileList)
            {
                FileInfo info = new FileInfo(file);
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
                RefreshPathComboBox();
            }
        }
        private FilesAndDirectories _selectedFileAndDirectory { get; set; } = new FilesAndDirectories();

        public FilesAndDirectories SelectedFileAndDirectory
        {
            get
            {
                return _selectedFileAndDirectory;
            }
            set
            {
                _selectedFileAndDirectory = value;
            }
        }
        private bool _isPasteEnabled { get; set; }

        public bool IsPasteEnabled
        {
            get
            {
                return _isPasteEnabled;
            }
            set
            {
                _isPasteEnabled = value;
                RaisePropertyChanged();
            }
        }

        public ICommand CutPathCommand { get; private set; }

        private bool isCut { get; set; } = false;

        private void ExecuteCutPath()
        {
            if (SelectedFileAndDirectory != null)
            {
                _CopiedDirOrFile = SelectedFileAndDirectory;
                isCut = true;
                IsPasteEnabled = true;
            }
        }
        public ICommand CopyPathCommand { get; private set; }

        private void ExecuteCopyPath()
        {
            if (SelectedFileAndDirectory != null)
            {
                _CopiedDirOrFile = SelectedFileAndDirectory;
                IsPasteEnabled = true;

            }
        }
        private FilesAndDirectories _CopiedDirOrFile { get; set; } = new FilesAndDirectories();

        public ICommand PastePathCommand { get; private set; }

        private void ExecutePastePath()
        {
            if (_CopiedDirOrFile.Path != null)
            {
                try
                {
                    if (_CopiedDirOrFile.Extension.ToLower() == "directory")
                    {
                        string destpath = CurrentPath + @"\"+_CopiedDirOrFile.Path.Split('\\').Last();
                        Functions.CopyFolder(_CopiedDirOrFile.Path,destpath);
                        if (isCut)
                        {
                            IsPasteEnabled = false;
                            Directory.Delete(_CopiedDirOrFile.Path);
                        }

                        RefreshAllCommand.Execute(true);

                    }
                    else
                    {
                        File.Copy(_CopiedDirOrFile.Path,
                            CurrentPath + @"\\" + _CopiedDirOrFile.Path.Split('\\').Last());
                        if (isCut)
                        {
                            IsPasteEnabled = false;
                            File.Delete(_CopiedDirOrFile.Path);
                        }
                    }

                    RefreshAllCommand.Execute(true);
                    isCut = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }   
            }

        }
        

        public string[] AddNewFileOrDirectory { get; set; } = {"Directory", "File"};
    }
}