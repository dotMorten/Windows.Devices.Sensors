using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Windows.Devices.Sensors;

namespace TestApp
{
	public class MainWindowVM : INotifyPropertyChanged
	{
		public MainWindowVM()
		{
			StartSensors();
		}

		private void StartSensors()
		{
			var compass = Windows.Devices.Sensors.Compass.GetDefault();
			if (compass != null)
			{
				compass.ReadingChanged += compass_ReadingChanged;
				CompassReading = compass.GetCurrentReading();
			}
			var accelerometer = Windows.Devices.Sensors.Accelerometer.GetDefault();
			if (accelerometer != null)
			{
				accelerometer.ReadingChanged += accelerometer_ReadingChanged;
				AccelerometerReading = accelerometer.GetCurrentReading();
			}
			var orientation = Windows.Devices.Sensors.OrientationSensor.GetDefault();
			if (orientation != null)
			{
				orientation.ReadingChanged += orentation_ReadingChanged;
				OrientationSensorReading = orientation.GetCurrentReading();
			}
		}

		#region Compass

		private CompassReading m_CompassReading;

		public CompassReading CompassReading
		{
			get { return m_CompassReading; }
			set { m_CompassReading = value; OnPropertyChanged(); }
		}

		private void compass_ReadingChanged(object sender, Windows.Devices.Sensors.CompassReadingChangedEventArgs e)
		{
			System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)delegate
			{
				CompassReading = e.Reading;
			});
		}

		#endregion Compass

		#region Accelerometer

		private AccelerometerReading m_AccelerometerReading;

		public AccelerometerReading AccelerometerReading
		{
			get { return m_AccelerometerReading; }
			set { m_AccelerometerReading = value; OnPropertyChanged(); }
		}

		private void accelerometer_ReadingChanged(object sender, Windows.Devices.Sensors.AccelerometerReadingChangedEventArgs e)
		{
			System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)delegate
			{
				AccelerometerReading = e.Reading;
			});
		}

		#endregion Accelerometer

		#region Orientation

		private OrientationSensorReading m_OrientationSensorReading;

		public OrientationSensorReading OrientationSensorReading
		{
			get { return m_OrientationSensorReading; }
			set { m_OrientationSensorReading = value; OnPropertyChanged(); }
		}

		private void orentation_ReadingChanged(object sender, Windows.Devices.Sensors.OrientationSensorReadingChangedEventArgs e)
		{
			System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)delegate
			{
				OrientationSensorReading = e.Reading;
			});
		}

		#endregion Orientation


		private void OnPropertyChanged([CallerMemberName]string propertyName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
