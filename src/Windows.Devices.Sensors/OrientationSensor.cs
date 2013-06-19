using System;
using Windows.Devices.Sensors.ObjectModel;

namespace Windows.Devices.Sensors
{
	public class OrientationSensor
	{
		/// <summary>
		/// Gets the default orientation sensor.
		/// </summary>
		/// <returns>
		/// The default orientation sensor or null if no orientation sensors are found.
		/// </returns>
		public static OrientationSensor GetDefault()
		{
			try
			{
				var sensorList = SensorManager.GetSensorsByTypeId<OrientationSensorInternal>();
				if (sensorList.Count > 0)
				{
					return new OrientationSensor(sensorList[0]);
				}
			}
			catch
			{
			}
			return null;
		}

		private Sensors.OrientationSensorInternal m_orientationSensor;

		private OrientationSensor(Sensors.OrientationSensorInternal orientationSensor)
		{
			m_orientationSensor = orientationSensor;
			m_orientationSensor.DataReportChanged += DataReportChanged;
		}

		private void DataReportChanged(Sensor sender, EventArgs e)
		{
			if (ReadingChanged != null)
			{
				ReadingChanged(this, new OrientationSensorReadingChangedEventArgs(GetCurrentReading()));
			}
		}
		public event EventHandler<OrientationSensorReadingChangedEventArgs> ReadingChanged;

		public OrientationSensorReading GetCurrentReading()
		{
			return m_orientationSensor.CurrentOrientation;
		}

		public uint MinimumReportInterval 
		{
			get { return m_orientationSensor.MinimumReportInterval; }
		}

		public uint ReportInterval
		{
			get { return m_orientationSensor.ReportInterval; }
			set { m_orientationSensor.ReportInterval = value; }
		}
	}

	public sealed class OrientationSensorReadingChangedEventArgs
	{
		internal OrientationSensorReadingChangedEventArgs(OrientationSensorReading reading)
		{
			Reading = reading;
		}
		public OrientationSensorReading Reading { get; private set; }
	}

	/// <summary>
	/// Represents an orientation-sensor reading.
	/// </summary>
	public sealed class OrientationSensorReading
	{
		private static Guid SensorDataKey = new Guid("1637d8a2-4248-4275-865d-558de84aedfd");

		/// <summary>
		/// Creates a new instance of this class.
		/// </summary>
		/// <param name="report">The sensor report to evaluate.</param>
		internal OrientationSensorReading(SensorReport report)
		{
			if (report == null) { throw new ArgumentNullException("report"); }
			byte[] q = report.Values[SensorDataKey][0] as byte[];
			byte[] m = report.Values[SensorDataKey][1] as byte[];
			if (q != null && q.Length == 16)
				Quaternion = new SensorQuaternion(q);
			if (m != null && m.Length == 36)
				RotationMatrix = new SensorRotationMatrix(m);
		}
		/// <summary>
		/// Gets the Quaternion for the current orientation-sensor reading.
		/// </summary>
		public SensorQuaternion Quaternion { get; private set; }

		/// <summary>
		/// Gets the rotation matrix for the current orientation-sensor reading. 
		/// </summary>
		public SensorRotationMatrix RotationMatrix { get; private set; }
	}
	/// <summary>
	/// Represents a Quaternion.
	/// </summary>
	public sealed class SensorQuaternion
	{
		internal SensorQuaternion(byte[] data)
		{
			X = BitConverter.ToSingle(data, 0);
			Y = BitConverter.ToSingle(data, 4);
			Z = BitConverter.ToSingle(data, 8);
			W = BitConverter.ToSingle(data, 12);
		}
		/// <summary>
		/// Gets the w-value of the Quaternion.
		/// </summary>
		public float W { get; set; }
		/// <summary>
		/// Gets the x-value of the Quaternion.
		/// </summary>
		public float X { get; set; }
		/// <summary>
		/// Gets the y-value of the Quaternion.
		/// </summary>
		public float Y { get; set; }
		/// <summary>
		/// Gets the z-value of the Quaternion.
		/// </summary>
		public float Z { get; set; }
	}

	/// <summary>
	/// Represents a 3x3 rotation matrix.
	/// </summary>
	public sealed class SensorRotationMatrix
	{
		internal SensorRotationMatrix(byte[] data)
		{
			M11 = BitConverter.ToSingle(data, 0);
			M12 = BitConverter.ToSingle(data, 4);
			M13 = BitConverter.ToSingle(data, 8);
			M21 = BitConverter.ToSingle(data, 12);
			M22 = BitConverter.ToSingle(data, 16);
			M23 = BitConverter.ToSingle(data, 20);
			M31 = BitConverter.ToSingle(data, 24);
			M32 = BitConverter.ToSingle(data, 28);
			M33 = BitConverter.ToSingle(data, 32);
		}

		/// <summary>Gets the value at row 1, column 1 of the given rotation matrix.</summary>
		public float M11 { get; set; }
		/// <summary>Gets the value at row 1, column 2 of the given rotation matrix.</summary>
		public float M12 { get; set; }
		/// <summary>Gets the value at row 1, column 3 of the given rotation matrix.</summary>
		public float M13 { get; set; }
		/// <summary>Gets the value at row 2, column 1 of the given rotation matrix.</summary>
		public float M21 { get; set; }
		/// <summary>Gets the value at row 2, column 2 of the given rotation matrix.</summary>
		public float M22 { get; set; }
		/// <summary>Gets the value at row 2, column 3 of the given rotation matrix.</summary>
		public float M23 { get; set; }
		/// <summary>Gets the value at row 3, column 1 of the given rotation matrix.</summary>
		public float M31 { get; set; }
		/// <summary>Gets the value at row 3, column 2 of the given rotation matrix.</summary>
		public float M32 { get; set; }
		/// <summary>Gets the value at row 3, column 3 of the given rotation matrix.</summary>
		public float M33 { get; set; }
	}
}
