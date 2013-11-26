using System;
using Windows.Devices.Sensors;

namespace XamlActions.Sensors {
    public class LightSensorImpl : ILightSensor {
        private LightSensor _sensor;

        public uint MinimumReportInterval {
            get { return _sensor.MinimumReportInterval; }
        }

        public uint ReportInterval {
            get { return _sensor.ReportInterval; }
            set { _sensor.ReportInterval = value; }
        }

        public bool IsAvailable {
            get {
                return _sensor != null;
            }
        }

        public LightSensorImpl() {
            _sensor = LightSensor.GetDefault();
            if (_sensor == null) {
                return;
            }
            _sensor.ReadingChanged += (sender, args) => {
                var reading = new LightSensorReport {
                    IlluminanceInLux = args.Reading.IlluminanceInLux,
                    Timestamp = args.Reading.Timestamp
                };
                if (ReadingChanged != null) {
                    ReadingChanged(reading);
                }
            };
        }

        public event Action<LightSensorReport> ReadingChanged;
    }
}