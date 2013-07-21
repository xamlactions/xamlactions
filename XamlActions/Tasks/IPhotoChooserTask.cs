using System;

namespace XamlActions.Tasks {
    public interface IPhotoChooserTask {
        int Height { get; set; }
        int Width { get; set; }
        void Show(Action<Photo> photoSelectedAction, Action photoNotSelectedAction, Action<Exception> errorAction);
    }
}