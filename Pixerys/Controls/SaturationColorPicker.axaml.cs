using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Media;

namespace Pixerys.Controls;

[TemplatePart("PART_SaturationPicker", typeof(ContentControl))]
public class SaturationColorPicker : TemplatedControl
{
    ContentControl? saturationPicker;

    private bool IsToggled = false;

    public static readonly DirectProperty<SaturationColorPicker, double> PosXProperty =
        AvaloniaProperty.RegisterDirect<SaturationColorPicker, double>(
            nameof(PosX),
            o => o.PosX,
            (o, v) => o.PosX = v,
            defaultBindingMode: BindingMode.TwoWay);

    private double _posX;

    public double PosX
    {
        get { return _posX; }
        set { SetAndRaise(PosXProperty, ref _posX, value); }
    }

    public static readonly DirectProperty<SaturationColorPicker, double> PosYProperty =
        AvaloniaProperty.RegisterDirect<SaturationColorPicker, double>(
            nameof(PosY),
            o => o.PosY,
            (o, v) => o.PosY = v,
            defaultBindingMode: BindingMode.TwoWay);

    private double _posY;

    public double PosY
    {
        get { return _posY; }
        set { SetAndRaise(PosYProperty, ref _posY, value); }
    }

    public static readonly DirectProperty<SaturationColorPicker, LinearGradientBrush> LinearGradientBlackProperty =
        AvaloniaProperty.RegisterDirect<SaturationColorPicker, LinearGradientBrush>(
            nameof(LinearGradientBlack),
            o => o.LinearGradientBlack);

    private LinearGradientBrush _linearGradientBlack = new()
    {
        StartPoint = new RelativePoint(new Point(0, 1), RelativeUnit.Relative),
        EndPoint = new RelativePoint(new Point(0, 0), RelativeUnit.Relative),

        GradientStops = [
            new GradientStop(new Color(255, 0, 0, 0), 0.0),
            new GradientStop(new Color(0, 0, 0, 0), 1.0),
        ],
    };

    public LinearGradientBrush LinearGradientBlack
    {
        get { return _linearGradientBlack; }
        set { SetAndRaise(LinearGradientBlackProperty, ref _linearGradientBlack, value); }
    }

    public static readonly DirectProperty<SaturationColorPicker, LinearGradientBrush> LinearGradientWhiteProperty =
        AvaloniaProperty.RegisterDirect<SaturationColorPicker, LinearGradientBrush>(
            nameof(LinearGradientWhite),
            o => o.LinearGradientWhite);

    private LinearGradientBrush _linearGradientWhite = new()
    {
        StartPoint = new RelativePoint(new Point(0, 0), RelativeUnit.Relative),
        EndPoint = new RelativePoint(new Point(1, 0), RelativeUnit.Relative),

        GradientStops = [
            new GradientStop(new Color(255, 255, 255, 255), 0.0),
            new GradientStop(new Color(0, 255, 255, 255), 1.0),
        ],
    };

    public LinearGradientBrush LinearGradientWhite
    {
        get { return _linearGradientWhite; }
        set { SetAndRaise(LinearGradientWhiteProperty, ref _linearGradientWhite, value); }
    }
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        saturationPicker = e.NameScope.Find("PART_SaturationPicker") as ContentControl;

        if (saturationPicker != null)
        {
            saturationPicker.PointerPressed += SaturationPicker_PointerPressed;
            saturationPicker.PointerMoved += SaturationPicker_PointerMoved;
            saturationPicker.PointerReleased += SaturationPicker_PointerReleased;
        }
    }

    private void SaturationPicker_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
    {
        IsToggled = false;
    }

    private void SaturationPicker_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        Point pos = e.GetPosition(saturationPicker);

        if (IsToggled == true)
        {
            PosX = pos.X;
            PosY = pos.Y;
        }
    }

    private void SaturationPicker_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        IsToggled = true;
    }
}