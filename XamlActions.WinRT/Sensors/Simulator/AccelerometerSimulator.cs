using System;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace XamlActions.Sensors.Simulator {
    public class AccelerometerSimulator : IAccelerometer {
        private string _emulatorIp;
        private int _port;
        private StreamSocket _socket;
        private DataReader _dataReader;

        public AccelerometerSimulator(string emulatorIp, int port) {
            _emulatorIp = emulatorIp;
            _port = port;
            Task.Run(() => Connect());
        }

        public async Task Connect() {
            _socket = new StreamSocket();
            await _socket.ConnectAsync(new HostName(_emulatorIp), _port.ToString());
            _dataReader = new DataReader(_socket.InputStream);
            await Task.Run(() => StartReadingAsync());
        }

        private async Task StartReadingAsync() {
            while (_socket != null) {
                await _dataReader.LoadAsync(sizeof (UInt32));
                var size = _dataReader.ReadUInt32();
                await _dataReader.LoadAsync(size);
                var json = _dataReader.ReadString(size);
                var reading = AccelerometerReportSerializer.FromString(json);
                if (ReadingChanged != null) {
                    ReadingChanged(reading);
                }
            }
        }

        public event Action<AccelerometerReport> ReadingChanged;

        public uint MinimumReportInterval {
            get { return 500; }
        }

        public uint ReportInterval { get; set; }

        public bool IsAvailable {
            get { return true; }
        }
    }
}