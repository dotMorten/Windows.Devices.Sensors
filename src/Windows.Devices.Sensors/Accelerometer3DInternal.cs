// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using Windows.Devices.Sensors.Metadata;
using Windows.Devices.Sensors.ObjectModel;
namespace Windows.Devices.Sensors
{
    /// <summary>
    /// Represents a 3D accelerometer.
    /// </summary>
    [SensorDescription("C2FB0F5F-E2D2-4C78-BCD0-352A9582819D")]
	internal class Accelerometer3DInternal : Sensor
    {
        /// <summary>
        /// Gets the current acceleration in G's. 
        /// </summary>
        public Acceleration3D CurrentAcceleration
        {
            get
            {
				if (this.DataReport == null && !base.TryUpdateData())
					return null;
				return new Acceleration3D(this.DataReport);
            }
        }
    }

    /// <summary>
    /// Specifies the axis of the acceleration measurement.
    /// </summary>
	internal enum AccelerationAxis
    {
        /// <summary>
        /// The x-axis.
        /// </summary>        
        XAxis = 0,
        /// <summary>
        /// The y-axis.
        /// </summary>        
        YAxis = 1,
        /// <summary>
        /// THe z-axis.
        /// </summary>        
        ZAxis = 2
    }

    /// <summary>
    /// Creates an acceleration measurement from the data in the report.
    /// </summary>
	internal class Acceleration3D
    {
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="report">The sensor report to evaluate.</param>
        public Acceleration3D(SensorReport report)
        {
            if (report == null) { throw new ArgumentNullException("report"); }
			object val = report.Values[SensorPropertyKeys.SensorDataTypeAccelerationXG.FormatId][0];
            this.acceleration[(int)AccelerationAxis.XAxis] =
                (double)report.Values[SensorPropertyKeys.SensorDataTypeAccelerationXG.FormatId][0];
            this.acceleration[(int)AccelerationAxis.YAxis] =
				(double)report.Values[SensorPropertyKeys.SensorDataTypeAccelerationYG.FormatId][1];
            this.acceleration[(int)AccelerationAxis.ZAxis] =
				(double)report.Values[SensorPropertyKeys.SensorDataTypeAccelerationZG.FormatId][2];
        }

        /// <summary>
        /// Gets the acceleration reported by the sensor.
        /// </summary>
        /// <param name="axis">The axis of the acceleration.</param>
        /// <returns></returns>
		public double this[AccelerationAxis axis]
        {
            get { return acceleration[(int)axis]; }
        }
		private double[] acceleration = new double[3];
    }
}
