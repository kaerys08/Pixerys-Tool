using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pixerys.ViewModels
{
    internal partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _name = "Kaerys";

        [RelayCommand]
        public void ButtonOnClick()
        {
            Name = "Kaerys Diaz";
        }
    }
}
