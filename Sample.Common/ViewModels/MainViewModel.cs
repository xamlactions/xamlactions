using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using XamlActions;

namespace Sample.Common.ViewModels {
    public class MainViewModel : ViewModelBase {

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
