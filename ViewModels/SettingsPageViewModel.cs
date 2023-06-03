using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using Location = Microsoft.Maui.Devices.Sensors.Location;

namespace Animalib.ViewModels
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        public SettingsPageViewModel() 
        {
            Battery.Default.BatteryInfoChanged += refreshBatteryInfoOnChange;                                 

            refreshBatteryInfo();
            refreshDisplayInfo();
            refreshDeviceInfo();
        }

        //BATTERY-INFO
        [ObservableProperty]
        public string batteryPercentage;
        [ObservableProperty]
        public string batteryStatus;
        [ObservableProperty]
        public string powerSource;
        [ObservableProperty]
        public string displayWidth;

        //DISPLAY-INFO
        [ObservableProperty]
        public string displayHeight;
        [ObservableProperty]
        public string displayDensity;
        [ObservableProperty]
        public string displayOrientation;
        [ObservableProperty]
        public string displayRotation;
        [ObservableProperty]
        public string displayRefreshRate;

        //DEVICE-INFO
        [ObservableProperty]
        public string devicePlatform;
        [ObservableProperty]
        public string deviceType;
        [ObservableProperty]
        public string deviceIdiom;
        [ObservableProperty]
        public string deviceModel;
        [ObservableProperty]
        public string deviceManufacturer;
        [ObservableProperty]
        public string deviceName;
        [ObservableProperty]
        public string deviceOsVersion;

        //GEO-LOCATION
        [ObservableProperty]
        public string currentLocationLongitude;
        [ObservableProperty]
        public string currentLocationLatitude;
        [ObservableProperty]
        public string latitudeTest;
        [ObservableProperty]
        public string longitudeTest;
        [ObservableProperty]
        public string addressTest;
        [ObservableProperty]
        public string retrievedAddress;

        [RelayCommand]
        public async void takePicture()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                FileResult picture = await MediaPicker.Default.CapturePhotoAsync();

                if (picture != null)
                {                    
                    var file = await picture.OpenReadAsync();
                    var imageData = new byte[file.Length];
                    await file.ReadAsync(imageData, 0, (int)file.Length);                    

                    var fileName = Path.ChangeExtension("animalib_image_" + DateTime.Now.Millisecond.ToString(), "jpg");
                    var savePath = Path.Combine(FileSystem.AppDataDirectory, fileName);                    

                    using (var fileToSave = File.Create(savePath)) 
                    {
                        await fileToSave.WriteAsync(imageData, 0, imageData.Length);
                        stopwatch.Stop();
                        await Toast.Make($"Task completed in: {stopwatch.Elapsed.TotalMilliseconds} ms", ToastDuration.Long).Show();
                    }
                }
            }
        }

        [RelayCommand]
        public async Task getCurrentLocation() 
        {
            try
            {                
                GeolocationRequest locationRequest = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                var cancelationTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(locationRequest, cancelationTokenSource.Token);

                if (location != null)
                {
                    CurrentLocationLatitude  = location.Latitude.ToString();
                    CurrentLocationLongitude = location.Longitude.ToString();
                }
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                await Toast.Make($"Unable to get current location, please make sure that location services are activated on your device!", ToastDuration.Long).Show();
            }
        }

        [RelayCommand]
        private async void getLocationByAddress(string address) 
        {
            try
            {
                IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(address);

                Location location = locations?.FirstOrDefault();

                if (location != null)
                {
                    LatitudeTest = location.Latitude.ToString();
                    LongitudeTest = location.Longitude.ToString();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                await Toast.Make($"Unable to get Location coordinates of: {address}", ToastDuration.Long).Show();
            }
        }

        [RelayCommand]
        private async void getAddressByLocation(string location) 
        {
            try 
            {
                double latitude = Convert.ToDouble(location.Split('/')[0]);
                double longitude = Convert.ToDouble(location.Split('/')[1]);

                IEnumerable<Placemark> placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude);

                Placemark placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    RetrievedAddress = $"{placemark.Locality}, {placemark.PostalCode} {placemark.SubLocality}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Toast.Make($"Unable to get Address by Location, please try again later!", ToastDuration.Long).Show();
            }
        }

        [RelayCommand]
        private void refreshBatteryInfo() 
        {
            BatteryPercentage = $"{Battery.Default.ChargeLevel * 100}%";

            BatteryStatus = Battery.Default.State switch
            {
                BatteryState.Charging => "Charging",
                BatteryState.Discharging => "Discharging",
                BatteryState.Full => "Full",
                BatteryState.NotCharging => "Not Charging",
                BatteryState.NotPresent => "Battery not available!",
                BatteryState.Unknown => "Unknown",
                _ => "Unknown"
            };

            PowerSource = Battery.Default.PowerSource switch
            {
                BatteryPowerSource.Wireless => "Wireless charging",
                BatteryPowerSource.Usb => "USB cable charging",
                BatteryPowerSource.AC => "Device is plugged in to a power source",
                BatteryPowerSource.Battery => "Device isn't charging",
                _ => "Unknown"
            };
        }

        private void refreshBatteryInfoOnChange(object sender, BatteryInfoChangedEventArgs e) 
        {
            BatteryPercentage = $"{e.ChargeLevel * 100}%";

            BatteryStatus = e.State switch
            {
                BatteryState.Charging => "Charging",
                BatteryState.Discharging => "Discharging",
                BatteryState.Full => "Full",
                BatteryState.NotCharging => "Not Charging",
                BatteryState.NotPresent => "Battery not available!",
                BatteryState.Unknown => "Unknown",
                _ => "Unknown"
            };

            PowerSource = e.PowerSource switch
            {
                BatteryPowerSource.Wireless => "Wireless charging",
                BatteryPowerSource.Usb => "USB cable charging",
                BatteryPowerSource.AC => "Device is plugged in to a power source",
                BatteryPowerSource.Battery => "Device isn't charging",
                _ => "Unknown"
            };
        }

        private void refreshDisplayInfo() 
        {
            DisplayWidth = DeviceDisplay.Current.MainDisplayInfo.Width + " Pixels";

            DisplayHeight = DeviceDisplay.Current.MainDisplayInfo.Height + " Pixels";

            DisplayOrientation = DeviceDisplay.Current.MainDisplayInfo.Orientation switch
            {
                Microsoft.Maui.Devices.DisplayOrientation.Portrait => "Portrait",
                Microsoft.Maui.Devices.DisplayOrientation.Landscape => "Landscape",
                Microsoft.Maui.Devices.DisplayOrientation.Unknown => "Unknown",
                _ => "Unknown"
            };

            DisplayRotation = DeviceDisplay.Current.MainDisplayInfo.Rotation switch
            {
                Microsoft.Maui.Devices.DisplayRotation.Rotation0 => "0 Degrees",
                Microsoft.Maui.Devices.DisplayRotation.Rotation90 => "90 Degrees",
                Microsoft.Maui.Devices.DisplayRotation.Rotation180 => "180 Degrees",
                Microsoft.Maui.Devices.DisplayRotation.Rotation270 => "270 Degrees",
                Microsoft.Maui.Devices.DisplayRotation.Unknown => "Unknown",
                _ => "Unknown"
            };

            DisplayRefreshRate = DeviceDisplay.Current.MainDisplayInfo.RefreshRate + " Hz";
        }

        private void refreshDeviceInfo() 
        {
            DevicePlatform = DeviceInfo.Current.Platform.ToString();

            DeviceType = DeviceInfo.Current.DeviceType switch
            {
                Microsoft.Maui.Devices.DeviceType.Physical => "Physical",
                Microsoft.Maui.Devices.DeviceType.Virtual => "Virtual",
                Microsoft.Maui.Devices.DeviceType.Unknown => "Unknown",
                _ => "Unknown"
            };

            DeviceIdiom = DeviceInfo.Current.Idiom.ToString();
            DeviceModel = DeviceInfo.Current.Model;
            DeviceManufacturer = DeviceInfo.Current.Manufacturer;
            DeviceName = DeviceInfo.Current.Name;
            DeviceOsVersion = DeviceInfo.Current.VersionString;
        }
    }
}
