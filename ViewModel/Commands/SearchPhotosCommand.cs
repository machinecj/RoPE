using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RoPE.ViewModel.Commands
{
    public class SearchPhotosCommand : ICommand
    {
        public RoPEViewModel VM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SearchPhotosCommand(RoPEViewModel vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            string selectedCamera = parameter as string;
            return !string.IsNullOrWhiteSpace(selectedCamera);
        }

        public void Execute(object parameter)
        {
            VM.SearchPhotos(parameter as string);
        }
    }
}
