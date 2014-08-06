using XamlActions;
namespace Sample.Universal.ViewModels {
    public class MainViewModel : ViewModelBase {

        private int _angle;

        public int Angle {
            get { return _angle; }
            set {
                _angle = value;
                RaisePropertyChanged(() => Angle);
            }
        }

        public void Rotate() {
            Angle += 90;
        }

        public void GoToDetail() {
            Navigator.NavigateTo(ViewKeys.Detail);
        }
    }
}
