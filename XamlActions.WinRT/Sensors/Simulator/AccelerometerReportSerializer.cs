using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamlActions.Sensors.Simulator {
    public static class AccelerometerReportSerializer {

        public static string ToString(AccelerometerReport report) {
            return String.Format("{0}|{1}|{2}|{3}", 
                report.AccelerationX, 
                report.AccelerationY, 
                report.AccelerationZ,
                report.Timestamp);
        }

        public static AccelerometerReport FromString(string serialized) {
            string[] values = serialized.Split('|');
            return new AccelerometerReport {
                AccelerationX = Convert.ToDouble(values[0]),
                AccelerationY = Convert.ToDouble(values[1]),
                AccelerationZ = Convert.ToDouble(values[2]),
                Timestamp = Convert.ToDateTime(values[3])
            };
        }
    }
}