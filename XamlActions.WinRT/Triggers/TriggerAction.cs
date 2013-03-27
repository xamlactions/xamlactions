using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace XamlActions.Triggers {
    public abstract class TriggerAction : FrameworkElement {
        public abstract void StartAction();
    }
}
