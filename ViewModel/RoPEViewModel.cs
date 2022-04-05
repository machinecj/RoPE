using RoPE.Model;
using RoPE.Model.Manifest;
using RoPE.ViewModel.Commands;
using RoPE.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RoPE.ViewModel
{
    public class RoPEViewModel : INotifyPropertyChanged
    {
        private string selectedRover;

        public string SelectedRover
        {
            get { return selectedRover; }
            set 
            { 
                selectedRover = value;
                OnPropertyChanged("SelectedRover");
            }
        }

        private int selectedSol;

        public int SelectedSol
        {
            get { return selectedSol; }
            set 
            { 
                selectedSol = value;
                OnPropertyChanged("SelectedSol");
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

        public SelectRoverCommand SelectRoverCommand { get; set; }

        public RoPEViewModel()
        {
            SelectedRover = "Spirit";
            PhotoManifest = new PhotoManifest
            {
                Name = "Spirit"
            };
            SelectRoverCommand = new SelectRoverCommand(this);
        }

        public async void MakePhotoManifest()
        {
            var manifests = await NASARoverPhotoAPIHelper.GetPhotoManifest(SelectedRover);


        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
