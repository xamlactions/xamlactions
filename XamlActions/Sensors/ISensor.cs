using System;

namespace XamlActions.Sensors {
    public interface ISensor {
        uint MinimumReportInterval { get; }
        uint ReportInterval { get; set; }
        bool IsAvailable { get;}
    }

    public interface IAccelerometer : ISensor {
        event Action<AccelerometerReport> ReadingChanged;
    }
    public interface IGyrometer : ISensor {
        event Action<GyrometerReport> ReadingChanged;
    }

    public interface ICompass : ISensor {
        event Action<CompassReport> ReadingChanged;
    }

    public interface ILightSensor : ISensor {
        event Action<LightSensorReport> ReadingChanged;
    }

    
}