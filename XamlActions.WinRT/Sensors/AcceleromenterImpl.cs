using System;
using Windows.Devices.Sensors;

namespace XamlActions.Sensors {

    public class AccelerometerImpl : IAccelerometer {
        private Accelerometer _sensor;

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

        public AccelerometerImpl() {
            _sensor = Accelerometer.GetDefault();
            if (_sensor == null) {
                return;
            }
            _sensor.ReadingChanged += (sender, args) => {
                var reading = new AccelerometerReport {
                    AccelerationX = args.Reading.AccelerationX,
                    AccelerationY = args.Reading.AccelerationY,
                    AccelerationZ = args.Reading.AccelerationZ,
                    Timestamp = args.Reading.Timestamp
                };
                if (ReadingChanged != null) {
                    ReadingChanged(reading);
                }
            };
        }

        public event Action<AccelerometerReport> ReadingChanged;
    }
}