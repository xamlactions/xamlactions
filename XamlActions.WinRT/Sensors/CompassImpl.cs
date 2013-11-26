using System;
using Windows.Devices.Sensors;

namespace XamlActions.Sensors {
    public class CompassImpl : ICompass {
        private Compass _sensor;

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

        public CompassImpl() {
            _sensor = Compass.GetDefault();
            if (_sensor == null) {
                return;
            }
            _sensor.ReadingChanged += (sender, args) => {
                var reading = new CompassReport {
                    HeadingMagneticNorth = args.Reading.HeadingMagneticNorth,
                    HeadingTrueNorth = args.Reading.HeadingTrueNorth,
                    Timestamp = args.Reading.Timestamp
                };
                if (ReadingChanged != null) {
                    ReadingChanged(reading);
                }
            };
        }

        public event Action<CompassReport> ReadingChanged;
    }
}