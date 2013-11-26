using System;

namespace XamlActions.Sensors {
    public class LightSensorReport : ISensorReport {
          public float IlluminanceInLux { get; set; }
          public DateTimeOffset Timestamp { get; set; }
    }
}
