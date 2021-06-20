using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerWPF.Models
{
    class FilesAndDirectories
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string DateEdited { get; set; }
        public string Extension { get; set; }
        public string Size { get; set; } = null;

    }
}
