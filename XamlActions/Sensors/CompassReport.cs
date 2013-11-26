using System;

namespace XamlActions.Sensors {
    public class CompassReport : ISensorReport {
        public double HeadingMagneticNorth { get; set; }
        public double? HeadingTrueNorth { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}