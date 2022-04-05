using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RoPE.ViewModel.Commands
{
    public class SelectRoverCommand : ICommand
    {
        public RoPEViewModel VM { get; set; }

        public event EventHandler CanExecuteChanged;

        public SelectRoverCommand(RoPEViewModel vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.MakePhotoManifest();
        }
    }
}
