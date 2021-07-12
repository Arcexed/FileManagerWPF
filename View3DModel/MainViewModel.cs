using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View3DModel
{
    public class MainViewModel
    {
        public string[] combobox { get; set; } = {"ABS"};
        public string[] ScaleCombobox { get; set; } = {"1:2", "1:5", "1:10", "1:20"};

        public string[] PrintersCombobox { get; set; } = {"Printer 1", "Printer 2"};


    }
}
