using System.Diagnostics;
using XamlActions;

namespace Sample.Common.ViewModels {
    public class MainViewModel : ViewModelBase {
        private int _angle;

        public int Angle {
            get { return _angle; }
            set {
                _angle = value;
                RaisePropertyChanged(() => Angle);
            }
        }

        public string Title { get; set; }

        public MainViewModel() {
            Title = "MainViewModel";
        }

        public void Loaded() {}

        public void Doit() {
            Debug.WriteLine("Doit");
        }

        public void DoitWithParam(object arg) {
            Debug.WriteLine("Doit:" + arg);
        }

        public void Rotate() {
            Angle += 90;
            Debug.WriteLine("Rotate:" + Angle);
        }

        public void GoToDetail() {
            Navigator.NavigateTo(ViewKeys.Detail);
        }
    }
}