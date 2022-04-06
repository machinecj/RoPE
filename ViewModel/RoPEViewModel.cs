using RoPE.Model;
using RoPE.Model.Manifest;
using RoPE.ViewModel.Commands;
using RoPE.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace RoPE.ViewModel
{
    public class RoPEViewModel : INotifyPropertyChanged
    {
        private int selectedSol;

        public int SelectedSol
        {
            get { return selectedSol; }
            set 
            {
                selectedSol = value;
                OnPropertyChanged("SelectedSol");
                MakeAvailableCameras();
            }
        }

        private string selectedCamera;

        public string SelectedCamera
        {
            get { return selectedCamera; }
            set 
            { 
                selectedCamera = value;
                if (selectedCamera != null)
                    OnPropertyChanged("SelectedCamera");
            }
        }

        private string selectedDate;

        public string SelectedDate
        {
            get { return selectedDate; }
            set 
            { 
                selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        private int countOfPhotosForSelectedSol;

        public int CountOfPhotosForSelectedSol
        {
            get { return countOfPhotosForSelectedSol; }
            set 
            { 
                countOfPhotosForSelectedSol = value;
                OnPropertyChanged("CountOfPhotosForSelectedSol");
            }
        }

        private bool isPhotoManifestSet;

        public bool IsPhotoManifestSet
        {
            get { return isPhotoManifestSet; }
            set 
            { 
                isPhotoManifestSet = value;
                OnPropertyChanged("IsPhotoManifestSet");
            }
        }




        private PhotoManifest photoManifest;

        public PhotoManifest PhotoManifest
        {
            get { return photoManifest; }
            set 
            { 
                photoManifest = value;
                OnPropertyChanged("PhotoManifest");
            }
        }

        public ObservableCollection<string> AvailableCameras { get; set; }

        public SelectRoverCommand SelectRoverCommand { get; set; }

        public RoPEViewModel()
        {
            SelectRoverCommand = new SelectRoverCommand(this);
            AvailableCameras = new ObservableCollection<string>();
        }

        public async void MakePhotoManifest(string roverName)
        {
            var manifest = await NASARoverPhotoAPIHelper.GetPhotoManifest(roverName);
            PhotoManifest = manifest;
            IsPhotoManifestSet = true;
            AvailableCameras.Clear();
            SelectedDate = "none";
            CountOfPhotosForSelectedSol = 0;
            SelectedSol = 0;
        }

        public void MakeAvailableCameras() // TODO refactor to move SelectedDate and CountOfPhotosForSelectedSol to seperate methods, or rename method
        {
            AvailableCameras.Clear();

            Model.Manifest.Photo photo = new Model.Manifest.Photo();
            foreach (Model.Manifest.Photo curPhoto in PhotoManifest.Photos)
            {
                if (curPhoto.Sol == SelectedSol)
                {
                    photo = curPhoto;
                    break;
                }
                else if (curPhoto.Sol > SelectedSol)
                { 
                    break; // TODO display some message when sol with no photos is selected?
                }
            }

            if (photo.Cameras == null)
            {
                SelectedDate = "none";
                CountOfPhotosForSelectedSol = 0;
            }
            else
            {
                foreach (string cameraName in photo.Cameras)
                    AvailableCameras.Add(cameraName);

                SelectedDate = photo.Earth_date;
                CountOfPhotosForSelectedSol = photo.Total_photos;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
