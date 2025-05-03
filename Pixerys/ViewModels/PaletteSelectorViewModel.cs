using CommunityToolkit.Mvvm.ComponentModel;

namespace Pixerys.ViewModels
{
    internal partial class PaletteSelectorViewModel : ViewModelBase
    {
        [ObservableProperty]
        private double _posX = 10;

        [ObservableProperty]
        private double _posY = 8;

        [ObservableProperty]
        private double _selectorAngle = 0.9375;

        [ObservableProperty]
        private double _posXSaturation = 0;

        [ObservableProperty]
        private double _posYSaturation = 0;

        [ObservableProperty]
        private byte _rComponent;

        [ObservableProperty]
        private byte _gComponent;

        [ObservableProperty]
        private byte _bComponent;
    }
}
