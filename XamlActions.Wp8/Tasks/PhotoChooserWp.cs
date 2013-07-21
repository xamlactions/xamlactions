using System;
using Microsoft.Phone.Tasks;

namespace XamlActions.Tasks {
    public class PhotoChooserTaskWp : IPhotoChooserTask {
        public int Height { get; set; }
        public int Width { get; set; }

        public void Show(Action<Photo> photoSelectedAction, Action photoNotSelectedAction, Action<Exception> errorAction) {
            try {
                var task = new PhotoChooserTask();
                task.ShowCamera = true;
                if (Height > 0) task.PixelHeight = Height;
                if (Width > 0) task.PixelHeight = Width;

                task.Completed += (sender, result) => {
                    if (result.TaskResult != TaskResult.OK) {
                        photoNotSelectedAction.Invoke();
                        return;
                    }
                    photoSelectedAction.Invoke(new Photo(result.OriginalFileName, result.ChosenPhoto));
                };
                task.Show();
            }
            catch (Exception ex) {
                errorAction.Invoke(ex);
            }
        }
    }
}