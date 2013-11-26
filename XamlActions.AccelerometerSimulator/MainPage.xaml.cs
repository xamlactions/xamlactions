using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.Networking.Connectivity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Microsoft.Devices.Sensors;
using Microsoft.Phone.Controls;
using XamlActions.Sensors;
using XamlActions.Sensors.Simulator;

namespace XamlActions.AccelerometerSimulator {
    public partial class MainPage : PhoneApplicationPage {
        private StreamSocket _client;
        private DataWriter _writer;

        // Constructor
        public MainPage() {
            InitializeComponent();
        }

        public async Task OpenServerAsync() {
            var listener = new StreamSocketListener();
            listener.ConnectionReceived += ListenerOnConnectionReceived;
            await listener.BindServiceNameAsync(Port.Text);
        }

        private void ListenerOnConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args) {
            if (_writer != null) {
                _writer.Dispose();
                _writer = null;
            }
            if (_client != null) {
                _client.Dispose();
                _client = null;
            }
            _client = args.Socket;
            _writer = new DataWriter(_client.OutputStream);
        }

        public async Task StartAccelerometerAsync() {
            var accelerometer = new Accelerometer();
            accelerometer.Start();
            accelerometer.TimeBetweenUpdates = TimeSpan.FromMilliseconds(500);
            accelerometer.CurrentValueChanged += AccelerometerCurrentValueChanged;
        }

        void AccelerometerCurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e) {
            if(_client == null || _writer == null) return;
            var report = new AccelerometerReport {
                AccelerationX = e.SensorReading.Acceleration.X,
                AccelerationY = e.SensorReading.Acceleration.Y,
                AccelerationZ = e.SensorReading.Acceleration.Z,
                Timestamp = e.SensorReading.Timestamp
            };
            try {
                var json = AccelerometerReportSerializer.ToString(report);
                var size = _writer.MeasureString(json);

            
                _writer.WriteUInt32(size);
                _writer.WriteString(json);
                _writer.StoreAsync();
                _writer.FlushAsync();
            }
            catch(Exception ex) {
                Debug.WriteLine("Error writing to stream: " + ex);
            }
        }

        public string FindLocalAddress() {
            var names = NetworkInformation.GetHostNames();
            var sb = new StringBuilder();
            sb.AppendLine("Listenning...");
            for (int i = 0; i < names.Count; i++) {
                sb.AppendFormat("IP-> {0}:{1}", names[i].DisplayName, Port.Text);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private async void StartAll(object sender, RoutedEventArgs e) {
            Start.IsEnabled = false;
            await OpenServerAsync();
            await StartAccelerometerAsync();
            Status.Text = FindLocalAddress();
        }
    }
}