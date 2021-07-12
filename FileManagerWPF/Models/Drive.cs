using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerWPF.Models
{
    class Drive
    {
        public string DriveInfo => $"{Name}\t{Type}\t{PersentUsedDiskSpace}%";
        public string Name { get; set; }

        public string Type { get; set; }
        public double UsedGB { get; set; }
        public double FreeGB { get; set; }
        public double TotalGB { get; set; }
        public int PersentUsedDiskSpace { get; set; }
        //

    }

}
