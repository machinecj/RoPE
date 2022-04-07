using RoPE.Model;
using RoPE.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RoPE.View
{
    /// <summary>
    /// Interaction logic for RoverWindow.xaml
    /// </summary>
    public partial class RoverWindow : Window
    {
        public RoverWindow()
        {
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ImageControl.MaxHeight = this.ActualHeight - 220;
            ImageControl.MaxWidth = this.ActualWidth;
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
