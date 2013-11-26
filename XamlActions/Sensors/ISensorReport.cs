using System;

namespace XamlActions.Sensors {
    public interface ISensorReport {
        DateTimeOffset Timestamp { get; set; }
    }
}