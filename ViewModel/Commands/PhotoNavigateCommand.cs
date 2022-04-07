using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RoPE.ViewModel.Commands
{
    public class PhotoNavigateCommand : ICommand
    {
        public RoPEViewModel VM { get; set; }

        public event EventHandler CanExecuteChanged;

        public PhotoNavigateCommand(RoPEViewModel vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var param = parameter as string;
            if (param.Equals("Left"))
                VM.DecrementCurrentPhotoIndex();
            else
                VM.IncrementCurrentPhotoIndex();
        }
    }
}
