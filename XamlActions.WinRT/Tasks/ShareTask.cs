using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;

namespace XamlActions.Tasks {
    public class ShareTask : IShareTask {

        public static ShareTask Default = new ShareTask();

        private DataTransferManager _dataTransferManager;
        private ShareItem _shareItem;

        public void Share(ShareItem shareItem) {
            _shareItem = shareItem;
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += DataTransferManagerOnDataRequested;
            DataTransferManager.ShowShareUI();
        }

        private void DataTransferManagerOnDataRequested(DataTransferManager sender, DataRequestedEventArgs args) {
            DataPackage data = args.Request.Data;
            data.Properties.Title = _shareItem.Title;
            if (_shareItem.Message != null) {
                data.Properties.Description = _shareItem.Message;
            }
            if (_shareItem.ImageUri != null) {
                data.SetBitmap(RandomAccessStreamReference.CreateFromUri(_shareItem.ImageUri));
            }
            if (_shareItem.Link != null) {
                data.SetUri(_shareItem.Link);
            }
            _dataTransferManager.DataRequested -= DataTransferManagerOnDataRequested;
        }
    }
}