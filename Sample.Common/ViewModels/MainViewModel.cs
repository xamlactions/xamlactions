using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using XamlActions;

namespace Sample.Common.ViewModels {
    public class MainViewModel : ViewModelBase {

        public string Title { get; set; }

        public MainViewModel() {
            Title = "MainViewModel";
        }

        public void Loaded() {
            
        }

        public void Doit() {
            Debug.WriteLine("Doit");
        }

        public void DoitWithParam(object arg) {
            Debug.WriteLine("Doit:" + arg);
        }
    }
}
