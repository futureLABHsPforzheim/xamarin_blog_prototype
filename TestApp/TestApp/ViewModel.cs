using Plugin.Geolocator;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace TestApp
{
    public class ViewModel : INotifyPropertyChanged
    {
        public Command getLocationCommand { get; }
        public Command getLocationAsyncCommand { get; }
        String latitude="?";
        String longitude="?";
        public Image image;
        String path;
        private static Page page = MainPage.Instance;
        public Command getTakePhotoAsyncCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ViewModel()
        {
            getLocationCommand = new Command(getLocation);
            getLocationAsyncCommand = new Command(async () => await getLocationAsync());
            getTakePhotoAsyncCommand = new Command(async () => await takePhotoAsync());
        }
        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public String Longitude
        {
            set
            {
                if (longitude != value)
                {
                    longitude = value;
                    
                    OnPropertyChanged("Longitude");
                }
            }
            get
            {
                return longitude;
            }
        }

        public String Latitude
        {
            set
            {
                if (latitude != value)
                {
                    latitude = value;
                    
                    OnPropertyChanged(nameof(Latitude));
                }
            }
            get
            {
                return latitude;
            }
        }

        public Image Image
        {
            set
            {
                if (image != value)
                {
                    image = value;

                    OnPropertyChanged(nameof(Image));
                }
            }
            get
            {
                return image;
            }
        }
        public String Path
        {
            set
            {
                if (path != value)
                {
                    path = value;

                    OnPropertyChanged(nameof(Path));
                }
            }
            get
            {
                return path;
            }
        }
        public void getLocation()
        {
            Debug.WriteLine("getLocation Button clicked");
            this.Longitude = "Test2";

        }
        public bool IsLocationAvailable()
        {
            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        

        public async System.Threading.Tasks.Task getLocationAsync()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            
            if (locator.IsGeolocationAvailable && locator.IsGeolocationEnabled)
            {
                var position = await locator.GetPositionAsync();
                Debug.WriteLine("Position Status: {0}", position.Timestamp);
                this.Longitude = "Longitude " + position.Longitude.ToString();
                this.Latitude = "Latitude " + position.Latitude.ToString();
                Debug.WriteLine(longitude);
                Debug.WriteLine(latitude);
            }



        }

        public async Task takePhotoAsync()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await page.DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file == null)
                    return;
                var aPpath = file.AlbumPath;
                await page.DisplayAlert("File Location", file.Path, "OK");
                this.Path = file.Path;
                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });

                //or:
                //image.Source = ImageSource.FromFile(file.Path);
                //image.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }


        }
    }
}
