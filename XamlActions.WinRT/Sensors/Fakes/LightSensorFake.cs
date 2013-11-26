using System;
using System.Threading.Tasks;
using Windows.Devices.Sensors;

namespace XamlActions.Sensors.Fakes {
    public class LightSensorFake : ILightSensor {

        private bool _running;
        public static float IlluminanceInLux { get; set; }

        public uint MinimumReportInterval { get; set; }

        public uint ReportInterval  { get; set; }
            
        public bool IsAvailable {
            get {
                return true;
            }
        }

        public LightSensorFake() {
            MinimumReportInterval = 500;
            ReportInterval = 500;
            
        }
        public void StartRunning() {
            if(_running) return;
            _running = true;
            Task.Run(() => StartTimer());
        }

        public void Stop() {
            _running = false;
        }

        public void Read(float illuminanceInLux) {
            var reading = new LightSensorReport {
                IlluminanceInLux = illuminanceInLux,
                Timestamp = DateTime.Now
            };
            if (ReadingChanged != null) {
                ReadingChanged(reading);
            }
        }

        public async Task StartTimer() {
            while (_running) {
                await Task.Delay((int)ReportInterval);
                Read(LightSensorFake.IlluminanceInLux);
            }
        }

        public event Action<LightSensorReport> ReadingChanged;
    }
}