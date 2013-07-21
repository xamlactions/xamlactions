using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;

namespace XamlActions.Tasks {
    //public class PhotoChooserWinRt : IPhotoChooserTask {
    //    public int Height { get; set; }
    //    public int Width { get; set; }

    //    public void Show(Action<Photo> photoSelectedAction, Action photoNotSelectedAction, Action<Exception> errorAction) {
    //        var cameraCaptureUi = new CameraCaptureUI();
    //        if (Height > 0 || Width > 0) {
    //            cameraCaptureUi.PhotoSettings.AllowCropping = true;
    //            cameraCaptureUi.PhotoSettings.CroppedSizeInPixels = new Size(Width, Height);
    //            IAsyncOperation<StorageFile> task = cameraCaptureUi.CaptureFileAsync(CameraCaptureUIMode.Photo);
    //            StorageFile x = task.AsTask().Result;
    //            x.
    //        }
    //    }
    //}
}
