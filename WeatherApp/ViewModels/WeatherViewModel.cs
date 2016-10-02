using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WeatherApp.Models;
using WeatherApp.Converters;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public class WeatherViewModel : ViewModelBase
    {
        private WeatherData weatherData;
        private WeatherService weatherService;
        private UnixTimeStampToLocalTimeZoneConverter localTimeZoneConverter;
        private UserSettings userSettings = new UserSettings();
        private DispatcherTimer dispatcherTimer;

        public WeatherViewModel(WeatherService weatherService)
        {
            UpdateCommand = new WeatherCommand();
            UpdateCommand.CanExecuteFunc = obj => true;
            UpdateCommand.ExecuteFunc = UpdateWeatherData;

            weatherData = new WeatherData();
            this.weatherService = weatherService;

            localTimeZoneConverter = new UnixTimeStampToLocalTimeZoneConverter();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMinutes(1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;

            if (userSettings.Location != string.Empty)
            {
                Location = userSettings.Location;
                UpdateWeatherData(null);
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdateWeatherData(Location);
            Debug.WriteLine("Update Interval!");
        }

        public string Location
        {
            get { return weatherData.Name; }
            set { weatherData.Name = value; }
        }

        public string Description
        {
            get { return weatherData.Description; }
        }

        public string Icon
        {
            get
            {
                if (weatherData.Icon == string.Empty || weatherData.Icon == null)
                    return "..\\Icons\\01d.png";

                return "..\\Icons\\" + weatherData.Icon + ".png";
            }
        }

        public float Temperature
        {
            get { return weatherData.Temperature.Temp; }
            set
            {
                weatherData.Temperature.Temp = value;
                OnPropertyChanged("Temperature");
            }
        }

        public int MinTemperature
        {
            get { return (int)weatherData.Temperature.Temp_Min; }
        }

        public int MaxTemperature
        {
            get { return (int)weatherData.Temperature.Temp_Max; }
        }

        public string Sunrise
        {
            get
            {
                string sunrise = localTimeZoneConverter.Convert(weatherData.Sunrise, typeof(string), Coordinates, null).ToString();

                return sunrise;
            }
        }

        public string Sunset
        {
            get
            {
                string sunset = localTimeZoneConverter.Convert(weatherData.Sunset, typeof(string), Coordinates, null).ToString();

                return sunset;
            }
        }

        public float WindSpeed
        {
            get { return weatherData.Wind.Speed; }
            set { weatherData.Wind.Speed = value; }
        }

        public int WindAngle
        {
            get { return weatherData.Wind.Deg; }
        }

        public int Humidity
        {
            get { return weatherData.Temperature.Humidity; }
            set { weatherData.Temperature.Humidity = value; }
        }

        public int Pressure
        {
            get { return weatherData.Temperature.Pressure; }
        }

        public double LastUpdateTime
        {
            get { return weatherData.Time; }
        }

        public WeatherCommand UpdateCommand { get; set; }

        private bool isUpdated;
        public bool IsUpdated
        {
            get { return isUpdated; }
            set
            {
                isUpdated = value;
                OnPropertyChanged("IsUpdated");
            }
        }

        private bool failedFindingLocation;
        public bool FailedFindingLocation
        {
            get { return failedFindingLocation; }
            set
            {
                failedFindingLocation = value;
                OnPropertyChanged("FailedFindingLocation");
            }
        }


        public Coordinates Coordinates
        {
            get { return weatherData.Coordinates; }
        }

        public void UpdateWeatherData(object parameter)
        {
            try
            {
                WeatherData newWeatherData = weatherService.getWeatherData(Location);

                FailedFindingLocation = newWeatherData == null;
                if (!FailedFindingLocation)
                {
                    IsUpdated = true;
                    weatherData = newWeatherData;
                    OnPropertyChanged(string.Empty);

                    SaveLocation();
                }

                if (!dispatcherTimer.IsEnabled)
                    dispatcherTimer.Start();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void SaveLocation()
        {
            userSettings.Location = Location;
            userSettings.Save();
        }
    }
}
