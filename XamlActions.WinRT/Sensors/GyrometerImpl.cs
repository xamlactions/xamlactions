using System;
using Windows.Devices.Sensors;

namespace XamlActions.Sensors {
    public class GyrometerImpl : IGyrometer {
        private Gyrometer _sensor;

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

        public GyrometerImpl() {
            _sensor = Gyrometer.GetDefault();
            if (_sensor == null) {
                return;
            }
            _sensor.ReadingChanged += (sender, args) => {
                var reading = new GyrometerReport {
                    AngularVelocityX = args.Reading.AngularVelocityX,
                    AngularVelocityY = args.Reading.AngularVelocityY,
                    AngularVelocityZ = args.Reading.AngularVelocityZ,
                    Timestamp = args.Reading.Timestamp
                };
                if (ReadingChanged != null) {
                    ReadingChanged(reading);
                }
            };
        }

        public event Action<GyrometerReport> ReadingChanged;
    }
}