// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using Windows.Devices.Sensors.Metadata;
using Windows.Devices.Sensors.ObjectModel;
namespace Windows.Devices.Sensors
{
    /// <summary>
    /// Represents an orientation sensor.
    /// </summary>
	[SensorDescription("CDB5D8F7-3CFD-41C8-8542-CCE622CF5D6E")] //"B84919FB-EA85-4976-8444-6F6F5C6D31DB")]
	internal class OrientationSensorInternal : Sensor
    {
        /// <summary>
        /// Gets the current device orientation. 
        /// </summary>
		public OrientationSensorReading CurrentOrientation
        {
            get
            {
				if (this.DataReport == null && !base.TryUpdateData())
					return null;
				return new OrientationSensorReading(this.DataReport);
            }
        }
    }
}
