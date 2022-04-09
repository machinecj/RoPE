using RoPE.Model;
using RoPE.Model.Manifest;
using RoPE.ViewModel.Commands;
using RoPE.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

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

        private bool isDisplayedPhotoSet;

        public bool IsDisplayedPhotoSet
        {
            get { return isDisplayedPhotoSet; }
            set 
            { 
                isDisplayedPhotoSet = value;
                OnPropertyChanged("IsDisplayedPhotoSet");
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

        private readonly string[] roverNames = { "Spirit", "Curiosity", "Opportunity", "Perseverance" };

        public List<PhotoManifest> PhotoManifests { get; set; }

        private List<string> photosList; 
        /* TODO photosList stores just a list of photo URLs for the sol/camera, so gets a new set of photos from the API each time a new camera is selected. 
         * Need to eventually keep all photos for the sol in another list of actual Photo objects, and if that list is already set for the sol, 
         * pull photos from that for the selected camera, instead or making another call to the api.
        */
        private string displayedPhoto;

        public string DisplayedPhoto
        {
            get { return displayedPhoto; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                    IsDisplayedPhotoSet = false;
                else
                    IsDisplayedPhotoSet = true;

                if (value.Contains(".jpl"))
                    displayedPhoto = value.Remove(12,4);
                else
                    displayedPhoto = value;
                OnPropertyChanged("DisplayedPhoto");
            }
        }

        private int currentPhotoIndex;

        public int CurrentPhotoIndex
        {
            get { return currentPhotoIndex; }
            set 
            {
                if (value > MaxPhotoIndex)
                    currentPhotoIndex = MaxPhotoIndex;
                else if (value < 0)
                    currentPhotoIndex = 0;
                else
                    currentPhotoIndex = value;

                if (photosList != null && photosList.Count > 0)
                {
                    DisplayedPhoto = photosList[currentPhotoIndex];
                }
                OnPropertyChanged("CurrentPhotoIndex");
            }
        }

        private int maxPhotoIndex;

        public int MaxPhotoIndex
        {
            get { return maxPhotoIndex; }
            set 
            { 
                maxPhotoIndex = value;
                OnPropertyChanged("MaxPhotoIndex");
            }
        }

        private bool isPhotoManifestsLoaded;

        public bool IsPhotoManifestsLoaded
        {
            get { return isPhotoManifestsLoaded; }
            set 
            { 
                isPhotoManifestsLoaded = value;
                OnPropertyChanged("IsPhotoManifestsLoaded");
            }
        }


        public ObservableCollection<string> AvailableCameras { get; set; }

        public SelectRoverCommand SelectRoverCommand { get; set; }

        public SearchPhotosCommand SearchPhotosCommand { get; set; }

        public PhotoNavigateCommand PhotoNavigateCommand { get; set; }

        public RoPEViewModel()
        {
            SelectRoverCommand = new SelectRoverCommand(this);
            SearchPhotosCommand = new SearchPhotosCommand(this);
            PhotoNavigateCommand = new PhotoNavigateCommand(this);
            AvailableCameras = new ObservableCollection<string>();
            photosList = new List<string>();
            PhotoManifests = new List<PhotoManifest>();
            BuildPhotoManifests();
        }

        public void SelectPhotoManifest(string roverName)
        {
            if (PhotoManifests.Count == 0)
                BuildPhotoManifests();

            foreach (PhotoManifest manifest in PhotoManifests)
            {
                if (manifest.Name.Equals(roverName))
                    PhotoManifest = manifest;
            }

            IsPhotoManifestSet = true;
            AvailableCameras.Clear();
            photosList.Clear();
            DisplayedPhoto = "";
            CurrentPhotoIndex = 0;
            MaxPhotoIndex = 0;
            SelectedDate = "none";
            CountOfPhotosForSelectedSol = 0;
            SelectedSol = 0;
            CommandManager.InvalidateRequerySuggested();
        }

        public async void BuildPhotoManifests()
        {
            PhotoManifests.Clear();
            foreach (string roverName in roverNames)
            {
                PhotoManifests.Add(await NASARoverPhotoAPIHelper.GetPhotoManifest(roverName));
            }
            IsPhotoManifestsLoaded = true;
        }

        public async void SearchPhotos(string cameraName)
        {
            photosList.Clear();
            var photos = await NASARoverPhotoAPIHelper.GetPhotos(PhotoManifest.Name, SelectedSol);

            foreach (Model.Photo photo in photos)
            {
                if (photo.Camera.Name.Equals(cameraName))
                    photosList.Add(photo.Img_src);
            }

            CurrentPhotoIndex = 0;
            MaxPhotoIndex = photosList.Count - 1;
        }

        public void MakeAvailableCameras()
        {
            if (PhotoManifest == null)
                return;

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
                    break;
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

        public void IncrementCurrentPhotoIndex()
        {
            if (CurrentPhotoIndex < MaxPhotoIndex)
                CurrentPhotoIndex++;
        }

        public void DecrementCurrentPhotoIndex()
        {
            if (CurrentPhotoIndex > 0)
                CurrentPhotoIndex--;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
