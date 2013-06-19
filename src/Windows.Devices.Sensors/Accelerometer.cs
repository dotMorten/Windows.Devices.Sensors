using System;
using Windows.Devices.Sensors.ObjectModel;

namespace Windows.Devices.Sensors
{
	public class Accelerometer
	{
		/// <summary>
		/// Gets the default accelerometer.
		/// </summary>
		/// <returns>
		/// The default accelerometer or null if no accelerometer is found.
		/// </returns>
		public static Accelerometer GetDefault()
		{
			try
			{
				var sensorList = SensorManager.GetSensorsByTypeId<Accelerometer3DInternal>();
				if (sensorList.Count > 0)
				{
					return new Accelerometer(sensorList[0]);
				}
			}
			catch
			{
			}
			return null;
		}

		private Sensors.Accelerometer3DInternal m_accelerometer;

		private Accelerometer(Sensors.Accelerometer3DInternal accelerometer)
		{
			m_accelerometer = accelerometer;
			m_accelerometer.DataReportChanged += DataReportChanged;
		}

		private void DataReportChanged(Sensor sender, EventArgs e)
		{
			if (ReadingChanged != null)
			{
				ReadingChanged(this, new AccelerometerReadingChangedEventArgs(GetCurrentReading()));
			}
		}
		public event EventHandler<AccelerometerReadingChangedEventArgs> ReadingChanged;

		public AccelerometerReading GetCurrentReading()
		{
			var reading = m_accelerometer.CurrentAcceleration;
			if (reading != null)
				return new AccelerometerReading(
					reading[AccelerationAxis.XAxis],
					reading[AccelerationAxis.YAxis],
					reading[AccelerationAxis.ZAxis]);
			else
				return new AccelerometerReading(double.NaN, double.NaN, double.NaN);
		}

		public uint MinimumReportInterval 
		{
			get { return m_accelerometer.MinimumReportInterval; }
		}

		public uint ReportInterval
		{
			get { return m_accelerometer.ReportInterval; }
			set { m_accelerometer.ReportInterval = value; }
		}
	}

	public sealed class AccelerometerReadingChangedEventArgs
	{
		internal AccelerometerReadingChangedEventArgs(AccelerometerReading reading)
		{
			Reading = reading;
		}
		public AccelerometerReading Reading { get; private set; }
	}

	public sealed class AccelerometerReading 
	{
		internal AccelerometerReading(double x, double y, double z)
		{
			AccelerationX = x;
			AccelerationY = y;
			AccelerationZ = z;
		}
		public double AccelerationX { get; private set; }
		public double AccelerationY { get; private set; }
		public double AccelerationZ { get; private set; }
	}
}
