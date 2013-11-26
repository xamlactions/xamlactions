using System;

namespace XamlActions.Sensors {
    public class GyrometerReport : ISensorReport {
        public double AngularVelocityX { get; set; }
        public double AngularVelocityY { get; set; }
        public double AngularVelocityZ { get; set; }
        public DateTimeOffset Timestamp { get; set; }

    }
}