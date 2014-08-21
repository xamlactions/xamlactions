using XamlActions;

namespace Sample.Common.ViewModels {
    public class ViewModelLocator : ViewModelLocatorBase {

        public MainViewModel MainViewModel { get; set; }
        public DetailViewModel DetailViewModel { get; set; }

        public override void CallFirst() {
        }

        public override void CallOnlyInRuntime() {
        }

        public override void CallOnlyInDesigntime() {
        }

        public override void CallLast() {
            MainViewModel = new MainViewModel();
            DetailViewModel = new DetailViewModel();
        }
    }
}
