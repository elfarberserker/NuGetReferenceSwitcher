using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel;

namespace NuGetReferenceSwitcher.Configuration
{
    public class Config : INotifyPropertyChanged
    {
        public string rootFolder { get; set; }
        public string nugetFilter { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
