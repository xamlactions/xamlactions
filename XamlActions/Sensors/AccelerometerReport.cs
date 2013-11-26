using System;

namespace XamlActions.Sensors {
    public class AccelerometerReport : ISensorReport {
        public double AccelerationX { get; set; }
        public double AccelerationY { get; set; }
        public double AccelerationZ { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}