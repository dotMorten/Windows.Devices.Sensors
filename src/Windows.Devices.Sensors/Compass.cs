using System;
using Windows.Devices.Sensors.ObjectModel;

namespace Windows.Devices.Sensors
{
	public class Compass
	{
		/// <summary>
		/// The default compass or null if no integrated compasses are found.
		/// </summary>
		public static Compass Default
		{
			get
			{
				try
				{
					var sensorList = SensorManager.GetSensorsByTypeId<CompassInternal>();
					if (sensorList.Count > 0)
					{
						return new Compass(sensorList[0]);
					}
				}
				catch
				{
				}
				return null;
			}
		}

		private Sensors.CompassInternal m_compass;

		private Compass(Sensors.CompassInternal compass)
		{
			m_compass = compass;
			m_compass.DataReportChanged += DataReportChanged;
		}

		private void DataReportChanged(Sensor sender, EventArgs e)
		{
			if (ReadingChanged != null)
			{
				var heading = m_compass.CurrentHeading;
				ReadingChanged(this, new CompassReadingChangedEventArgs(
					new CompassReading(heading, double.NaN, DateTimeOffset.Now)));
			}
		}
		public event EventHandler<CompassReadingChangedEventArgs> ReadingChanged;

		public CompassReading GetCurrentReading()
		{
			var heading = m_compass.CurrentHeading;
			return new CompassReading(heading, double.NaN, DateTimeOffset.Now);
		}

		public uint MinimumReportInterval 
		{
			get { return m_compass.MinimumReportInterval; }
		}

		public uint ReportInterval
		{
			get { return m_compass.ReportInterval; }
			set { m_compass.ReportInterval = value; }
		}
	}

	public sealed class CompassReadingChangedEventArgs
	{
		internal CompassReadingChangedEventArgs(CompassReading reading)
		{
			Reading = reading;
		}
		public CompassReading Reading { get; private set; }
	}

	public sealed class CompassReading
	{
		internal CompassReading(double magneticHeading, double trueHeading, DateTimeOffset timestamp)
		{
			HeadingMagneticNorth = magneticHeading;
			HeadingTrueNorth = trueHeading;
			Timestamp = timestamp;
		}
		public double HeadingMagneticNorth { get; private set; }
		public double HeadingTrueNorth { get; private set; }
		public DateTimeOffset Timestamp { get; private set; }
	}
}
