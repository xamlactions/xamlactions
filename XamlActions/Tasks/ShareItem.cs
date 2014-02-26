using System;

namespace XamlActions.Tasks {
    public class ShareItem {
        public string Title { get; set; }
        public string Message { get; set; }
        public Uri Link { get; set; }
        public Uri ImageUri { get; set; }

        public ShareItem(string title) {
            Title = title;
        }
    }
}