using System;
using Windows.Devices.Sensors.Metadata;
using Windows.Devices.Sensors.ObjectModel;

namespace Windows.Devices.Sensors
{
	[SensorDescription("76B5CE0D-17DD-414D-93A1-E127F40BDF6E")]
	internal class CompassInternal : Sensor
	{
		public CompassInternal() : base() { }

		public double CurrentHeading
		{
			get
			{
				if (this.DataReport == null && !base.TryUpdateData())
					return double.NaN;
				return GetHeading(this.DataReport);
			}
		}
		private double GetHeading(SensorReport report)
		{
			if (report == null) { throw new ArgumentNullException("report"); }
			var g = new Guid("1637d8a2-4248-4275-865d-558de84aedfd");
			object val = report.Values[g][0];
			return (double)val;
		}
	}
}
