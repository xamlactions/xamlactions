using System.IO;

namespace XamlActions.Tasks {
    public class Photo {
        public string Filename { get; set; }
        public Stream Stream { get; set; }

        public Photo(string filename, Stream stream) {
            Filename = filename;
            Stream = stream;
        }
    }
}