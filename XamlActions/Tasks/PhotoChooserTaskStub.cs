using System;

namespace XamlActions.Tasks {
    public class PhotoChooserTaskStub : IPhotoChooserTask {
        public int Height { get; set; }
        public int Width { get; set; }

        private Photo _photo;
        private bool _throwException;

        public void ConfigureToSelectPhoto(Photo photo) {
            _photo = photo;
        }

        public void ConfigureToNotSelectPhoto() {
            _photo = null;
            _throwException = false;
        }

        public void ConfigureToThrowException() {
            _photo = null;
            _throwException = true;
        }
        
        public void Show(Action<Photo> photoSelectedAction, Action photoNotSelectedAction, Action<Exception> errorAction) {
            if (_photo != null) {
                photoSelectedAction.Invoke(_photo);
                return;                
            }
            if (_throwException) {
                errorAction.Invoke(new Exception());
                return;
            }
            photoNotSelectedAction.Invoke();
        }
    }
}