using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamlActions;

namespace Sample.Common.ViewModels {
    public class ViewModelLocator : ViewModelLocatorBase {

        public MainViewModel MainViewModel { get; set; }

        public override void CallFirst() {
            MainViewModel = new MainViewModel();
        }

        public override void CallOnlyInRuntime() {
        }

        public override void CallOnlyInDesigntime() {
        }

        public override void CallLast() {
        }
    }
}
