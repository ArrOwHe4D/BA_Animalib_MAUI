using CommunityToolkit.Maui.Core;
using Toast = CommunityToolkit.Maui.Alerts.Toast;

namespace Animalib.Pages
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void SwitchFlashlight_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (SwitchFlashlight.IsToggled)
                {
                    await Flashlight.TurnOnAsync();
                }
                else
                {
                    await Flashlight.TurnOffAsync();
                }
            }
            catch (FeatureNotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
                await Toast.Make("Flashlight is not supported on this device!", ToastDuration.Long).Show();
            }
            catch (PermissionException ex)
            {
                Console.WriteLine(ex.Message);
                await Toast.Make("Insufficient permissions to toggle the flashlight!", ToastDuration.Long).Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Toast.Make("Unable to control the flashlight on this device!", ToastDuration.Long).Show();
            }
        }

        private async void SwitchGyroscope_Toggled(object sender, ToggledEventArgs e)
        {
            if (Gyroscope.Default.IsSupported)
            {
                if (!Gyroscope.Default.IsMonitoring)
                {
                    Gyroscope.Default.ReadingChanged += Gyroscope_ReadingChanged;
                    Gyroscope.Default.Start(SensorSpeed.UI);
                }
                else
                {
                    Gyroscope.Default.Stop();
                    Gyroscope.Default.ReadingChanged -= Gyroscope_ReadingChanged;
                }
            }
            else 
            {
                await Toast.Make("Gyroscope is not supported on this device!", ToastDuration.Long).Show();
            }
        }

        private void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            LabelGyroscope.Text = $"{e.Reading}";
        }

        private async void SwitchBarometer_Toggled(object sender, ToggledEventArgs e)
        {
            if (Barometer.Default.IsSupported)
            {
                if (!Barometer.Default.IsMonitoring)
                {
                    Barometer.Default.ReadingChanged += Barometer_ReadingChanged;
                    Barometer.Default.Start(SensorSpeed.UI);
                }
                else
                {
                    Barometer.Default.Stop();
                    Barometer.Default.ReadingChanged -= Barometer_ReadingChanged;
                }
            }
            else 
            {
                await Toast.Make("Barometer is not supported on this device!", ToastDuration.Long).Show();
            }
        }

        private void Barometer_ReadingChanged(object sender, BarometerChangedEventArgs e)
        {                        
            LabelBarometer.Text = $"{e.Reading}";
        }

        private async void SwitchAccelerometer_Toggled(object sender, ToggledEventArgs e)
        {
            if (Accelerometer.Default.IsSupported)
            {
                if (!Accelerometer.Default.IsMonitoring)
                {                    
                    Accelerometer.Default.ReadingChanged += Accelerometer_ReadingChanged;
                    Accelerometer.Default.Start(SensorSpeed.UI);
                }
                else
                {                    
                    Accelerometer.Default.Stop();
                    Accelerometer.Default.ReadingChanged -= Accelerometer_ReadingChanged;
                }
            }
            else
            {
                await Toast.Make("Accelerometer is not supported on this device!", ToastDuration.Long).Show();
            }
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {                        
            LabelAccelerometer.Text = $"{e.Reading}";
        }

        private async void SwitchMagnetometer_Toggled(object sender, ToggledEventArgs e)
        {
            if (Magnetometer.Default.IsSupported)
            {
                if (!Magnetometer.Default.IsMonitoring)
                {
                    Magnetometer.Default.ReadingChanged += Magnetometer_ReadingChanged;
                    Magnetometer.Default.Start(SensorSpeed.UI);
                }
                else
                {
                    Magnetometer.Default.Stop();
                    Magnetometer.Default.ReadingChanged -= Magnetometer_ReadingChanged;
                }
            }
            else
            {
                await Toast.Make("Magnetometer is not supported on this device!", ToastDuration.Long).Show();
            }
        }

        private void Magnetometer_ReadingChanged(object sender, MagnetometerChangedEventArgs e)
        {                        
            LabelMagnetometer.Text = $"{e.Reading}";
        }

        private async void ButtonReadCompass_Clicked(object sender, EventArgs e)
        {
            if (Compass.Default.IsSupported)
            {
                if (!Compass.Default.IsMonitoring)
                {
                    Compass.Default.ReadingChanged += Compass_ReadingChanged;
                    Compass.Default.Start(SensorSpeed.UI);
                }
                else
                {
                    Compass.Default.Stop();
                    Compass.Default.ReadingChanged -= Compass_ReadingChanged;
                }
            }
            else
            {
                await Toast.Make("Compass is not supported on this device!", ToastDuration.Long).Show();
            }
        }

        private void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            LabelCompass.Text = $"{e.Reading}";
        }
    }
}
