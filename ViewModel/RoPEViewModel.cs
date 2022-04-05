using RoPE.Model;
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

        public RoPEViewModel()
        {

        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
